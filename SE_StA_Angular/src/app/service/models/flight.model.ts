import { Service } from "./service.model";

export interface Flight {
  flightId: number,
  flightNumber: string,
  destination: string,
  serviceId: number,
  service: Service
}
