import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Contact } from './models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  constructor(private http: HttpClient) { }

  getContact(id: number): Contact {
    //return this.http.get<Contact>(`https://localhost:50001/api/contacts/${id}`);
    return {
      id: id,
      title: "Hotel",
      telefone: "12345678",
      addresses: [
        {
          id: 1,
          street: "Hauptstraße",
          houseNr: "10B",
          postcode: "85333",
          city: "München",
          addition: "",
          country: {
            id: 1,
            name: "Deutschland",
            language: "Deutsch",
            iso2: "DE",
            timezones: [
              {
                id: 1,
                name: "MEZ",
                diffUtc: 1
              }
            ]
          },
          timezone: {
            id: 1,
            name: "MEZ",
            diffUtc: 1
          }
        }
      ]
    };
  }
}
