import { Service } from "./service.model";

export interface RentalCar {
  rentalCarId: number,
  carModel: string,
  seats: string,
  service: Service
}
