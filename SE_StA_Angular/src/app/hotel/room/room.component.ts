import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';

import { Room } from '../models/room.model';
import { RoomService } from './room.service';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.scss']
})
export class RoomComponent implements OnInit, OnDestroy {
  hotelId = 0;
  rooms: Room[] = [];
  private subs: Subscription[] = [];
  constructor(private route: ActivatedRoute, private roomService: RoomService) { }

  ngOnInit() {
    this.subs.push(this.route.params.subscribe(params => {
      this.hotelId = +params['id']; //read id from route and convert into number

      this.subs.push(this.roomService.getRooms().pipe(
        catchError((error) => {
          const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
          return throwError(() => new Error(errorMsg));
        })
      ).subscribe((response) => {
        response.forEach((room) => {
          if (room.hotelId === this.hotelId) {
            this.subs.push(this.roomService.getRoomType(room.roomTypeId).pipe(
              catchError((error) => {
                const errorMsg = "Error " + error.status + " - " + error.statusText + " " + JSON.stringify(error.error);
                return throwError(() => new Error(errorMsg));
              })
            ).subscribe((response) => {
              room.roomType = response;
              this.rooms.push(room);
            }));
          }
        });
      }));
    }));
  }

  ngOnDestroy() {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }
}
