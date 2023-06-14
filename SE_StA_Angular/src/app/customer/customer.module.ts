import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { CustomerComponent } from './customer.component';
import { BookingComponent } from './booking/booking.component';

import { BookingService } from './booking/booking.service';
import { CustomerService } from './customer.service';

const routes: Routes = [
  { path: '', component: CustomerComponent, pathMatch: 'full' },
  { path: ':id/bookings', component: BookingComponent }
];

@NgModule({
  declarations: [
    CustomerComponent,
    BookingComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    BookingService,
    CustomerService
  ]
})
export class CustomerModule { }
