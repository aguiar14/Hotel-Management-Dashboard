import { Component, inject, OnInit } from '@angular/core';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { AsyncPipe } from '@angular/common';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RoomService } from '../../services/room-service';
import { BookingService } from '../../services/booking-service';
import { combineLatest, Observable } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
  imports: [
    AsyncPipe,
    MatGridListModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
  ],
})
export class DashboardComponent implements OnInit {
  private breakpointObserver = inject(BreakpointObserver);

  totalRooms: number = 0;
  totalBooking: number = 0;
  totalRevenue: number = 0;
  constructor(
    private _roomService: RoomService,
    private _bookingService: BookingService
  ) {}

  ngOnInit(): void {
    combineLatest([
      this._roomService.getRooms(),
      this._bookingService.gettAll(),
    ]).subscribe(([rooms, booking]) => {
      this.totalRooms = rooms.totalCount;
      this.totalBooking = booking.totalCount;
      this.totalRevenue = booking.items.reduce(
        (sum, b) => (sum += b.totalPrice),
        0
      );
    });
  }

  cards = this.breakpointObserver.observe(Breakpoints.Handset).pipe(
    map(({ matches }) => {
      if (matches) {
        return [
          {
            title: 'Total Revenue',
            cols: 1,
            rows: 1,
            content: this.totalRevenue,
          },
          {
            title: 'Total Rooms',
            cols: 1,
            rows: 1,
            content: this.totalRooms,
          },
          {
            title: 'Card 3',
            cols: 1,
            rows: 1,
            content: 'content goes here',
          },
          {
            title: 'Total Booking',
            cols: 1,
            rows: 1,
            content: this.totalBooking,
          },
        ];
      }

      return [
        {
          title: 'Total Revenue',
          cols: 2,
          rows: 1,
          content: this.totalRevenue,
        },
        {
          title: 'Total Rooms',
          cols: 1,
          rows: 1,
          content: this.totalRooms,
        },
        { title: 'Card 3', cols: 1, rows: 2, content: 'content goes here' },
        {
          title: 'Total Booking',
          cols: 1,
          rows: 1,
          content: this.totalBooking,
        },
      ];
    })
  );

  /** Based on the screen size, switch from standard to one column per row */
}
