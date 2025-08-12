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
import { AddUpdateBookingDialog } from '../../dialogs/add-update-booking-dialog/add-update-booking-dialog';
import { Booking } from '../../models/booking.model';

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
export class BookingComponenet implements OnInit {
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
    public _dialogService: DialogService
  ) {
    this.data = new BookingDataSource(_bookingService, _dialogService);
  }

  ngOnInit(): void {
    this.data.loadBookings();
  }

  add() {
    var booking = {
      notes: '',
      customer: undefined,
      room: undefined,
      checkInDate: '',
      checkOutDate: '',
      status: '',
      totalPrice: '',
    };

    const dialogRef = this._dialogService.open(AddUpdateBookingDialog, {
      data: booking,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined && result !== null) {
        console.log(result);
        this._bookingService.add(result).subscribe({
          next: (res) => this.data.loadBookings(),

          error: (err) => {
            console.error('err: ', err);
          },
        });
      }
    });
  }

  cancel(arg0: any) {
    throw new Error('Method not implemented.');
  }
  edit(bookingEdit: Booking) {
    const dialogRef = this._dialogService.open(AddUpdateBookingDialog, {
      data: bookingEdit,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result != undefined && result !== null) {
        this._bookingService
          .edit(bookingEdit.id, result)
          .subscribe(() => this.data.loadBookings());
      }
    });
  }
}
