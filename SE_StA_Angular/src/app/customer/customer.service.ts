import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Customer } from './models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private http: HttpClient) { }

  getCustomers(): Customer[] {
    //return this.http.get<Customer[]>('https://localhost:50001/api/customers');
    return [
      {
        id: 1,
        nr: "1",
        firstName: "Max",
        lastName: "Mustermann",
        dateOfBirth: "2023-06-14",
        contactId: 1,
        userId: 1
      }, {
        id: 2,
        nr: "2",
        firstName: "Maxi",
        lastName: "Mustermann",
        dateOfBirth: "2023-06-15",
        contactId: 2,
        userId: 2
      }
    ];
  }
}
