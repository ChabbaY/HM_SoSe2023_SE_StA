import { Component, OnInit, OnDestroy } from '@angular/core';
import { catchError, Subscription, throwError } from 'rxjs';

import { ServiceService } from '../service.service';
import { Wellness } from '../models/wellness.model';

@Component({
  selector: 'app-wellness',
  templateUrl: './wellness.component.html',
  styleUrls: ['./wellness.component.scss']
})
export class WellnessComponent implements OnInit, OnDestroy {
  wellnesses: Wellness[] = [];
  private subs: Subscription[] = [];
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.subs.push(this.serviceService.getWellnesses().pipe(
      catchError((error) => {
        const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.wellnesses = response;
      this.wellnesses.forEach((wellness) => {
        this.subs.push(this.serviceService.getService(wellness.serviceId).pipe(
          catchError((error) => {
            const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
            return throwError(() => new Error(errorMsg));
          })
        ).subscribe((response) => {
          wellness.service = response;

          this.subs.push(this.serviceService.getServiceType(response.serviceTypeId).pipe(
            catchError((error) => {
              const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
              return throwError(() => new Error(errorMsg));
            })
          ).subscribe((response) => {
            wellness.service.serviceType = response;
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
