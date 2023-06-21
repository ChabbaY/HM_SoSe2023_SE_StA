import { RoomType } from "./room-type.model";

export interface Room {
    id: number,
    hotelId: number,
    roomType: RoomType,
    nr: string
}
