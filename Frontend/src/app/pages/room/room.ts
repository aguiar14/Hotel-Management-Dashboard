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
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { DialogService } from '../../services/dialog-service';
import { AddUpdateRoomDialog } from '../../dialogs/add-update-room-dialog/add-update-room-dialog';
import { Room } from '../../models/room.model';

@Component({
  selector: 'app-room',
  imports: [
    MatProgressSpinnerModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
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
    'actions',
  ];

  constructor(
    private _roomService: RoomService,
    private dialogService: DialogService
  ) {
    this.data = new RoomDataSource(_roomService);
  }

  ngOnInit(): void {
    this.data.loadRooms();
  }

  add() {
    var room = {
      number: '',
      description: '',
      capacity: 1,
      pricePerNight: 50,
      isAvailable: true,
      roomType: undefined,
    };

    const dialogRef = this.dialogService.open(AddUpdateRoomDialog, {
      data: room,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this._roomService.add(result).subscribe((a) => this.data.loadRooms());
      }
    });
  }

  edit(roomEdit: Room) {
    const dialogRef = this.dialogService.open(AddUpdateRoomDialog, {
      data: roomEdit,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== null && result !== undefined) {
        this._roomService.edit(roomEdit.id, result).subscribe(() => {
          this.data.loadRooms();
        });
      }
    });
  }

  deleteRoom(arg0: any) {
    throw new Error('Method not implemented.');
  }
}
