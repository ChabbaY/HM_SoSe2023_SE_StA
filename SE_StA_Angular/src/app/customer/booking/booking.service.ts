import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Booking } from '../models/booking.model';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  constructor(private http: HttpClient) { }

  getBookings(customerId: number): Booking[] {
    //return this.http.get<Booking[]>(`https://localhost:50001/api/customers/${customerId}/bookings`);
    return [
      {
        bookingId: 1,
        number: "1",
        date: "2023-06-14",
        price: 100,
        customerId: customerId,
        invoiceId: 1,
        paymentMethodId: 1,
        statusId: 1
      }, {
        bookingId: 2,
        number: "2",
        date: "2023-06-16",
        price: 100.50,
        customerId: customerId,
        invoiceId: 2,
        paymentMethodId: 2,
        statusId: 2
      }
    ];
  }
}
