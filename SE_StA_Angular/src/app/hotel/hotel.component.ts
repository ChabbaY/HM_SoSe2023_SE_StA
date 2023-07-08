import { Component, OnInit, OnDestroy } from '@angular/core';
import { catchError, Subscription, throwError } from 'rxjs';

import { HotelService } from './hotel.service';
import { Hotel } from './models/hotel.model';

@Component({
  selector: 'app-hotel',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.scss']
})
export class HotelComponent implements OnInit, OnDestroy {
  hotels: Hotel[] = [];
  private subs: Subscription[] = [];
  constructor (private hotelService: HotelService) {}

  ngOnInit() {
    this.subs.push(this.hotelService.getHotels().pipe(
      catchError((error) => {
        const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.hotels = response;
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
