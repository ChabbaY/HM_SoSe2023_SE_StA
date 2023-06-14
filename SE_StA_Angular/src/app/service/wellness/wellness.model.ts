import { Service } from "../service.model";

export interface Wellness {
    id: number,
    name: string,
    duration: string,
    service: Service
}