import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';

import { ContactService } from './contact.service';
import { Contact } from './models/contact.model';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit, OnDestroy {
  contact!: Contact;
  private subs: Subscription[] = [];
  constructor(private route: ActivatedRoute, private contactService: ContactService) { }

  ngOnInit() {
    this.subs.push(this.route.params.subscribe(params => {
      const contactId = +params['id']; //read id from route and convert into number

      this.subs.push(this.contactService.getContact(contactId).pipe(
        catchError((error) => {
          const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
          return throwError(() => new Error(errorMsg));
        })
      ).subscribe((response) => {
        this.contact = response;
        this.contact.addresses = [];

        this.subs.push(this.contactService.getContactAddresses().pipe(
          catchError((error) => {
            const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
            return throwError(() => new Error(errorMsg));
          })
        ).subscribe((response) => {
          let contactAddresses = response;
          let addressIds: number[] = [];
          contactAddresses.forEach((contactAddress) => {
            if (contactAddress.contactId === this.contact.contactId) {
              addressIds.push(contactAddress.addressId);
            }
          })

          this.subs.push(this.contactService.getAddresses().pipe(
            catchError((error) => {
              const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
              return throwError(() => new Error(errorMsg));
            })
          ).subscribe((response) => {
            response.forEach((address) => {
              if (addressIds.includes(address.addressId)) {
                this.subs.push(this.contactService.getCountry(address.countryId).pipe(
                  catchError((error) => {
                    const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
                    return throwError(() => new Error(errorMsg));
                  })
                ).subscribe((response) => {
                  address.country = response;

                  this.subs.push(this.contactService.getTimezone(address.timeZoneId).pipe(
                    catchError((error) => {
                      const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
                      return throwError(() => new Error(errorMsg));
                    })
                  ).subscribe((response) => {
                    address.timezone = response;
                    this.contact.addresses.push(address);
                  }))
                }));
              }
            });
          }));
        }));
      }));
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
