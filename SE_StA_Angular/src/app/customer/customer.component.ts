import { Component, OnInit } from '@angular/core';
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];

  constructor (private customerService: CustomerService) {}

  ngOnInit() {
    this.customers = this.customerService.getCustomers();
  }
}
