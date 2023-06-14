import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

import { Contact } from './models/contact.model';
import { ContactService } from './contact.service';

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
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
