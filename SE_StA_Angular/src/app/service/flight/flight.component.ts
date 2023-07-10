import { Component, OnDestroy, OnInit } from '@angular/core';
import { catchError, Subscription, throwError } from 'rxjs';

import { ServiceService } from '../service.service';
import { Flight } from '../models/flight.model';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.scss']
})
export class FlightComponent implements OnInit, OnDestroy {
  flights: Flight[] = [];
  private subs: Subscription[] = [];
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.subs.push(this.serviceService.getFlights().pipe(
      catchError((error) => {
        const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.flights = response;
      this.flights.forEach((flight) => {
        this.subs.push(this.serviceService.getService(flight.serviceId).pipe(
          catchError((error) => {
            const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
            return throwError(() => new Error(errorMsg));
          })
        ).subscribe((response) => {
          flight.service = response;

          this.subs.push(this.serviceService.getServiceType(response.serviceTypeId).pipe(
            catchError((error) => {
              const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
              return throwError(() => new Error(errorMsg));
            })
          ).subscribe((response) => {
            flight.service.serviceType = response;
          }));
        }));
      });
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
