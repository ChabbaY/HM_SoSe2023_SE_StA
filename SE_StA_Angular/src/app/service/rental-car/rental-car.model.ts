import { Service } from "../service.model";

export interface RentalCar {
    id: number,
    carModel: string,
    service: Service
}