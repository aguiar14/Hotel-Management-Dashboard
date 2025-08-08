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
import { RoomsFilter } from './rooms.filter';
import { RoomTypeService } from '../../services/room-type-service';
import { RoomType } from '../../models/roomtype.model';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';

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
    MatInputModule,
    MatSelectModule,
    FormsModule,
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

  filter = new RoomsFilter();
  roomCapacities: number[] = [1, 2, 3, 4, 5];
  roomTypes: RoomType[] = [];

  constructor(
    private _roomService: RoomService,
    private dialogService: DialogService,
    private roomTypeService: RoomTypeService
  ) {
    this.data = new RoomDataSource(_roomService, dialogService, this.filter);
  }

  ngOnInit(): void {
    this.data.loadRooms();
    this.roomTypeService
      .getAll()
      .subscribe((response) => (this.roomTypes = response.items));
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
        this._roomService.add(result).subscribe(() => this.data.loadRooms());
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

  deleteRoom(id: number) {
    throw new Error('Method not implemented.');
  }
}
