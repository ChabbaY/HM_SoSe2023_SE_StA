import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { ContactComponent } from './contact.component';
import { ContactService } from './contact.service';

const routes: Routes = [
  { path: ':id', component: ContactComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    ContactComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    ContactService
  ]
})
export class ContactModule { }
