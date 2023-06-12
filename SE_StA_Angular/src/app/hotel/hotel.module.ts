import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { HotelComponent } from './hotel.component';
import { RoomComponent } from './room/room.component';
import { HotelService } from './hotel.service';
import { RoomService } from './room/room.service';

const routes: Routes = [
  { path: '', component: HotelComponent, pathMatch: 'full' },
  { path: ':id/rooms', component: RoomComponent }
];

@NgModule({
  declarations: [
    HotelComponent,
    RoomComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class HotelModule { }
