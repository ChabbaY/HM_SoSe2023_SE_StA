import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { urlConstant } from 'src/constants/url-constant';

import { Address } from './models/address.model';
import { Contact } from './models/contact.model';
import { ContactAddress } from './models/contact-address.model';
import { Country } from './models/country.model';
import { Timezone } from './models/timezone.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  constructor(private http: HttpClient) { }

  getContact(id: number) {
    return this.http.get<Contact>(`${urlConstant.apiPath}/api/contacts/${id}`);
  }

  getAddresses() {
    return this.http.get<Address[]>(`${urlConstant.apiPath}/api/addresses`);
  }
  getContactAddresses() {
    return this.http.get<ContactAddress[]>(`${urlConstant.apiPath}/api/contactAddresses`);
  }
  getCountry(id: number) {
    return this.http.get<Country>(`${urlConstant.apiPath}/api/countries/${id}`);
  }
  getTimezone(id: number) {
    return this.http.get<Timezone>(`${urlConstant.apiPath}/api/timeZones/${id}`);
  }
}
