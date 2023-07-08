import { Component, OnInit, OnDestroy } from '@angular/core';
import { catchError, Subscription, throwError } from 'rxjs';

import { CustomerService } from './customer.service';
import { Customer } from './models/customer.model';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit, OnDestroy {
  customers: Customer[] = [];
  private subs: Subscription[] = [];
  constructor (private customerService: CustomerService) {}

  ngOnInit() {
    this.subs.push(this.customerService.getCustomers().pipe(
      catchError((error) => {
        const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
        return throwError(() => new Error(errorMsg));
      })
    ).subscribe((response) => {
      this.customers = response;
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
