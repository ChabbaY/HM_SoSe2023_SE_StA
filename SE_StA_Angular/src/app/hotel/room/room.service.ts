import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from './room.model';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private http: HttpClient) { }

  getRooms(hotelId: number) {
    //return this.http.get<Room[]>(`https://localhost:50001/api/hotels/${hotelId}/rooms`);
    return [ {id: 1, hotelId: 1, roomTypeId: 1, nr: "1A"}, {id: 2, hotelId: 2, roomtypeId: 2, nr: "2B"} ]
  }
}
