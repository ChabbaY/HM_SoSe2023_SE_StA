import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { FlightComponent } from './flight/flight.component';
import { RentalCarComponent } from './rental-car/rental-car.component';
import { WellnessComponent } from './wellness/wellness.component';
import { ServiceComponent } from './service.component';

import { ServiceService } from './service.service';

const routes: Routes = [
  { path: '', component: ServiceComponent, children: [
    { path: '', component: FlightComponent, pathMatch: 'full' },
    { path: 'car', component: RentalCarComponent },
    { path: 'wellness', component: WellnessComponent }
  ]}
];

@NgModule({
  declarations: [
    FlightComponent,
    RentalCarComponent,
    WellnessComponent,
    ServiceComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    ServiceService
  ]
})
export class ServiceModule { }
