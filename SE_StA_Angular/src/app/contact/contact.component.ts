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

      this.contact = this.contactService.getContact(contactId);
      /*this.subs.push(this.contactService.getContact(contactId).pipe(
        catchError((error) => {
          const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
          return throwError(() => new Error(errorMsg));
        })
      ).subscribe((response) => {
        this.contact = response;
      }));*/
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
