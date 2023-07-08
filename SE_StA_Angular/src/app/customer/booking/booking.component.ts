import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

import { BookingService } from './booking.service';
import { Booking } from '../models/booking.model';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit, OnDestroy {
  customerId = 0;
  bookings: Booking[] = [];
  private subs: Subscription[] = [];
  constructor(private route: ActivatedRoute, private bookingService: BookingService) { }

  ngOnInit() {
    this.subs.push(this.route.params.subscribe(params => {
      this.customerId = +params['id']; //read id from route and convert into number

      this.bookings = this.bookingService.getBookings(this.customerId);
      /*this.subs.push(this.bookingService.getBookings(this.customerId).pipe(
        catchError((error) => {
          const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
          return throwError(() => new Error(errorMsg));
        })
      ).subscribe((response) => {
        this.bookings = response;
      }));*/
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
