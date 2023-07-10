import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Booking } from '../models/booking.model';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  constructor(private http: HttpClient) { }

  getBookings() {
    return this.http.get<Booking[]>(`${urlConstant.apiPath}/api/bookings`);
  }
}
