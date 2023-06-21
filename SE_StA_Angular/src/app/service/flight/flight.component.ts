import { Component, OnInit } from '@angular/core';
import { Flight } from '../models/flight.model';
import { ServiceService } from '../service.service';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.scss']
})
export class FlightComponent implements OnInit {
  flights: Flight[] = [];
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.flights = this.serviceService.getFlights();
  }
}
