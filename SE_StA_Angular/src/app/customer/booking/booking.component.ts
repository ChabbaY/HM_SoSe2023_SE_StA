import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

import { Booking } from './booking.model';
import { BookingService } from './booking.service';

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

      this.bookings = this.bookingService.getRooms(this.customerId);
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
