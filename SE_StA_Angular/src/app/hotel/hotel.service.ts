import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Hotel } from './models/hotel.model';

@Injectable({
  providedIn: 'root'
})
export class HotelService {
  constructor(private http: HttpClient) { }

  getHotels(): Hotel[] {
    //return this.http.get<Hotel[]>(`${urlConstant.apiPath}/api/hotels`);
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
