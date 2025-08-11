import { Component, inject, OnInit } from '@angular/core';

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
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { ConfirmDeleteDialog } from '../../dialogs/confirm-delete-dialog/confirm-delete-dialog';
import { BookingDataSource } from './booking.datasource';
import { BookingService } from '../../services/booking-service';

@Component({
  selector: 'app-booking',
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
  templateUrl: './booking.html',
  styleUrl: './booking.scss',
})
export class Booking implements OnInit {
  data: BookingDataSource;
  resultsLength: number = 10;

  displayedColumns: string[] = [
    'notes',
    'customerName',
    'customerEmail',
    'room',
    'checkInDate',
    'checkOutDate',
    'status',
    'totalPrice',
    'actions',
  ];

  constructor(
    private _bookingService: BookingService,
    _dialogService: DialogService
  ) {
    this.data = new BookingDataSource(_bookingService, _dialogService);
  }

  ngOnInit(): void {
    this.data.loadBookings();
  }

  cancel(arg0: any) {
    throw new Error('Method not implemented.');
  }
  edit(_t81: any) {
    throw new Error('Method not implemented.');
  }
}
