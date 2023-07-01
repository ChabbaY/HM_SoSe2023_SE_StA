import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Hotel } from './models/hotel.model';

@Injectable({
  providedIn: 'root'
})
export class HotelService {
  constructor(private http: HttpClient) { }

  getHotels() {
    return this.http.get<Hotel[]>(`${urlConstant.apiPath}/api/hotels`);
  }
}
