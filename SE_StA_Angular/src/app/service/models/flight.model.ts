import { Service } from "./service.model";

export interface Flight {
    id: number,
    flightNr: string,
    service: Service
}
