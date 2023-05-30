import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { DatenschutzComponent } from './datenschutz/datenschutz.component';
import { ImpressumComponent } from './impressum/impressum.component';

const routes: Routes = [
  { path: '', redirectTo: 'impressum', pathMatch: 'full' },
  { path: 'datenschutz', component: DatenschutzComponent },
  { path: 'impressum', component: ImpressumComponent }
];

@NgModule({
  declarations: [
    DatenschutzComponent,
    ImpressumComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
    DatenschutzComponent,
    ImpressumComponent
  ]
})
export class InfoModule { }
