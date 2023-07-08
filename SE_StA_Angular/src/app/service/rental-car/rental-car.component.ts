import { Component, OnInit, OnDestroy } from '@angular/core';
import { catchError, Subscription, throwError } from 'rxjs';

import { ServiceService } from '../service.service';
import { RentalCar } from '../models/rental-car.model';

@Component({
  selector: 'app-rental-car',
  templateUrl: './rental-car.component.html',
  styleUrls: ['./rental-car.component.scss']
})
export class RentalCarComponent implements OnInit, OnDestroy {
  rentalCars: RentalCar[] = [];
  private subs: Subscription[] = [];
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.rentalCars = this.serviceService.getRentalCars();
    /*this.subs.push(this.serviceService.getRentalCars().pipe(
      catchError((error) => {
        const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.rentalCars = response;
    }));*/
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
