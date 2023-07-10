import { Service } from "./service.model";

export interface Wellness {
  wellnessId: number,
  name: string,
  duration: string,
  serviceId: number,
  service: Service
}
