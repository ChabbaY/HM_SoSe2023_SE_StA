import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from '../models/room.model';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  constructor(private http: HttpClient) { }

  getRooms(hotelId: number): Room[] {
    //return this.http.get<Room[]>(`https://localhost:50001/api/hotels/${hotelId}/rooms`);
    return [
      {
        id: 1,
        hotelId: hotelId,
        roomType: {
          id: 1,
          name: "normal",
          defaultPrice: 100,
          personsCount: 2
        },
        nr: "1A"
      }, {
        id: 2,
        hotelId: hotelId,
        roomType: {
          id: 2,
          name: "special",
          defaultPrice: 120,
          personsCount: 2
        },
        nr: "2B"
      }
    ];
  }
}
