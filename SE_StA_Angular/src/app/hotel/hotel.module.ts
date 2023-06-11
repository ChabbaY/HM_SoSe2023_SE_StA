import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { RouterModule, Routes } from '@angular/router';

import { HotelComponent } from './hotel.component';
import { RoomComponent } from './room/room.component';
import { HotelService } from './hotel.service';
import { RoomService } from './room/room.service';

@NgModule({
  declarations: [
    HotelComponent,
    RoomComponent
  ],
  imports: [
    CommonModule
  ]
})
export class HotelModule { }
