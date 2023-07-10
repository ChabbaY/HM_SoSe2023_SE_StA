import { ServiceType } from "./service-type.model";

export interface Service {
  serviceId: number,
  serviceTypeId: number,
  serviceType: ServiceType
}
