import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Flight } from './models/flight.model';
import { RentalCar } from './models/rental-car.model';
import { Wellness } from './models/wellness.model';
import { Service } from './models/service.model';
import { ServiceType } from './models/service-type.model';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  constructor(private http: HttpClient) { }

  getFlights() {
    return this.http.get<Flight[]>(`${urlConstant.apiPath}/api/flights`);
  }

  getRentalCars() {
    return this.http.get<RentalCar[]>(`${urlConstant.apiPath}/api/rentalCars`);
  }

  getWellnesses() {
    return this.http.get<Wellness[]>(`${urlConstant.apiPath}/api/wellnesses`);
  }

  getService(id: number) {
    return this.http.get<Service>(`${urlConstant.apiPath}/api/services/${id}`);
  }
  getServiceType(id: number) {
    return this.http.get<ServiceType>(`${urlConstant.apiPath}/api/serviceTypes/${id}`);
  }
}
