import { Component, OnInit } from '@angular/core';
import { RentalCar } from '../models/rental-car.model';
import { ServiceService } from '../service.service';

@Component({
  selector: 'app-rental-car',
  templateUrl: './rental-car.component.html',
  styleUrls: ['./rental-car.component.scss']
})
export class RentalCarComponent implements OnInit {
  rentalCars: RentalCar[] = [];
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.rentalCars = this.serviceService.getRentalCars();
  }
}
