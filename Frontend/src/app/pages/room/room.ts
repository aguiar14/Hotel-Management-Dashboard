import { Component, inject, OnInit } from '@angular/core';
import { RoomDataSource } from './room.datasource';
import { RoomService } from '../../services/room-service';
//----------------------

import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule, SortDirection } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-room',
  imports: [
    MatProgressSpinnerModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    HttpClientModule,
  ],
  templateUrl: './room.html',
  styleUrl: './room.scss',
})
export class RoomComponent implements OnInit {
  data!: RoomDataSource;
  resultsLength: number = 3;
  displayedColumns: string[] = [
    'number',
    'description',
    'capacity',
    'price/night',
    'available',
    'type',
  ];

  constructor(private _roomService: RoomService) {
    this.data = new RoomDataSource(_roomService);
  }

  ngOnInit(): void {
    this.data.loadRooms();
  }
}
