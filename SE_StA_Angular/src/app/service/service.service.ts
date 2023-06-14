import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Flight } from './flight/flight.model';
import { RentalCar } from './rental-car/rental-car.model';
import { Wellness } from './wellness/wellness.model';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  constructor(private http: HttpClient) { }

  getFlights(): Flight[] {
    //return this.http.get<Flight[]>('https://localhost:50001/api/services/flights');
    return [
      {
        id: 1,
        flightNr: "1",
        service: {
          id: 1,
          serviceType: {
            id: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }, {
        id: 2,
        flightNr: "2",
        service: {
          id: 2,
          serviceType: {
            id: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }
    ];
  }

  getRentalCars(): RentalCar[] {
    //return this.http.get<RentalCar[]>('https://localhost:50001/api/services/rentalcars');
    return [
      {
        id: 1,
        carModel: "A",
        service: {
          id: 3,
          serviceType: {
            id: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }
    ];
  }

  getWellnesses(): Wellness[] {
    //return this.http.get<Wellness[]>('https://localhost:50001/api/services/wellnesses');
    return [
      {
        id: 1,
        name: "C",
        duration: "2h",
        service: {
          id: 4,
          serviceType: {
            id: 1,
            name: "abc",
            defaultPrice: 100
          }
        }
      }
    ];
  }
}
