import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Room } from '../models/room.model';
import { RoomType } from '../models/room-type.model';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  constructor(private http: HttpClient) { }

  getRooms() {
    return this.http.get<Room[]>(`${urlConstant.apiPath}/api/rooms`);
  }
  getRoomType(id: number) {
    return this.http.get<RoomType>(`${urlConstant.apiPath}/api/roomTypes/${id}`);
  }
}
