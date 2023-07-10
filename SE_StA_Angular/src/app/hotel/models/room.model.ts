import { RoomType } from "./room-type.model";

export interface Room {
  roomId: number,
  hotelId: number,
  roomTypeId: number,
  roomType: RoomType,
  roomNumber: string
}
