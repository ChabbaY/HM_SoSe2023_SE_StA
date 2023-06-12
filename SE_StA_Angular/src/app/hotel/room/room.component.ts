import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Room } from './room.model';
import { RoomService } from './room.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {
  hotelId = 0;
  private sub: any;
  rooms: Room[] = [];
  constructor(private route: ActivatedRoute, private roomService: RoomService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.hotelId = +params['id']; //read id from route and convert into number

      this.rooms = this.roomService.getRooms(this.hotelId);
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
