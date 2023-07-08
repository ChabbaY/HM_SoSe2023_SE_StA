import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Flight } from './models/flight.model';
import { RentalCar } from './models/rental-car.model';
import { Wellness } from './models/wellness.model';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  constructor(private http: HttpClient) { }

  getFlights(): Flight[] {
    //return this.http.get<Flight[]>(`${urlConstant.apiPath}/api/services/flights`);
    return [
      {
        flightId: 1,
        flightNumber: "1",
        destination: "Nirvana",
        service: {
          serviceId: 1,
          serviceType: {
            serviceTypeId: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }, {
        flightId: 2,
        flightNumber: "2",
        destination: "Oktoberfest",
        service: {
          serviceId: 2,
          serviceType: {
            serviceTypeId: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }
    ];
  }

  getRentalCars(): RentalCar[] {
    //return this.http.get<RentalCar[]>(`${urlConstant.apiPath}/api/services/rentalcars`);
    return [
      {
        rentalCarId: 1,
        carModel: "A",
        seats: "2",
        service: {
          serviceId: 3,
          serviceType: {
            serviceTypeId: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }
    ];
  }

  getWellnesses(): Wellness[] {
    //return this.http.get<Wellness[]>(`${urlConstant.apiPath}/api/services/wellnesses`);
    return [
      {
        wellnessId: 1,
        name: "C",
        duration: "2h",
        service: {
          serviceId: 4,
          serviceType: {
            serviceTypeId: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }
    ];
  }
}
