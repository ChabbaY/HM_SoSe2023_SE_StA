import { Service } from "./service.model";

export interface Flight {
  flightId: number,
  flightNumber: string,
  destination: string,
  service: Service
}
