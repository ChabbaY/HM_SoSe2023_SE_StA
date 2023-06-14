import { Component, OnInit } from '@angular/core';
import { Wellness } from '../models/wellness.model';
import { ServiceService } from '../service.service';

@Component({
  selector: 'app-wellness',
  templateUrl: './wellness.component.html',
  styleUrls: ['./wellness.component.scss']
})
export class WellnessComponent implements OnInit {
  wellnesses: Wellness[] = [];
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.wellnesses = this.serviceService.getWellnesses();
  }
}
