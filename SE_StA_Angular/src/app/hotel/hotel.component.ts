import { Component, OnInit } from '@angular/core';
import { Hotel } from './models/hotel.model';
import { HotelService } from './hotel.service';

@Component({
  selector: 'app-hotel',
  templateUrl: './hotel.component.html',
  styleUrls: ['./hotel.component.scss']
})
export class HotelComponent implements OnInit {
  hotels: Hotel[] = [];

  constructor (private hotelService: HotelService) {}

  ngOnInit() {
    this.hotels = this.hotelService.getHotels();
  }
}
