import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { NavComponent } from './nav.component';
import { NavIconComponent } from './nav-icon/nav-icon.component';
import { NavListComponent } from './nav-list/nav-list.component';

@NgModule({
  declarations: [
    NavComponent,
    NavIconComponent,
    NavListComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    NavComponent
  ]
})
export class NavModule { }
