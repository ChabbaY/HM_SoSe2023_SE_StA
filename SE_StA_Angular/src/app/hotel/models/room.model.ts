import { RoomType } from "./room-type.model";

export interface Room {
  roomId: number,
  hotelId: number,
  roomType: RoomType,
  roomNumber: string
}
