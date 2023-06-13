import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Hotel } from './hotel.model';

@Injectable({
  providedIn: 'root'
})
export class HotelService {
  constructor(private http: HttpClient) { }

  getHotels(): Hotel[] {
    //return this.http.get<Hotel[]>('https://localhost:50001/api/hotels');
    return [
      {
        id: 1,
        contactId: 1,
        name: "A"
      }, {
        id: 2,
        contactId: 2,
        name: "B"
      }
    ];
  }
}
