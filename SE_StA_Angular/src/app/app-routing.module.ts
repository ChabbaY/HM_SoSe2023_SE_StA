import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule) },
  { path: 'contact', loadChildren: () => import('./contact/contact.module').then(mod => mod.ContactModule) },
  { path: 'customers', loadChildren: () => import('./customer/customer.module').then(mod => mod.CustomerModule) },
  { path: 'hotels', loadChildren: () => import('./hotel/hotel.module').then(mod => mod.HotelModule) },
  { path: 'info', loadChildren: () => import('./info/info.module').then(mod => mod.InfoModule) },
  { path: 'services', loadChildren: () => import('./service/service.module').then(mod => mod.ServiceModule) },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
