import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Contact } from './models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  constructor(private http: HttpClient) { }

  getContact(id: number): Contact {
    //return this.http.get<Contact>(`${urlConstant.apiPath}/api/contacts/${id}`);
    return {
      contactId: id,
      salutation: "Hotel",
      phone: "12345678",
      addresses: [
        {
          addressId: 1,
          street: "Hauptstraße",
          houseNumber: "10B",
          postalCode: "85333",
          town: "München",
          addressAddition: "",
          country: {
            countryId: 1,
            name: "Deutschland",
            language: "Deutsch",
            iso2: "DE",
            timezones: [
              {
                timeZoneId: 1,
                name: "MEZ",
                difUtc: 1
              }
            ]
          },
          timezone: {
            timeZoneId: 1,
            name: "MEZ",
            difUtc: 1
          }
        }
      ]
    };
  }
}
