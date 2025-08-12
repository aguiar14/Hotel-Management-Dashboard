import {
  Component,
  inject,
  OnInit,
  ChangeDetectionStrategy,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { CommonModule, AsyncPipe } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { combineLatest, map, Observable } from 'rxjs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { startWith } from 'rxjs/operators';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialog,
  MatDialogRef,
  MatDialogTitle,
  MatDialogContent,
} from '@angular/material/dialog';

import {
  FormBuilder,
  FormGroup,
  FormsModule,
  Validators,
} from '@angular/forms';
import { CustomerService } from '../../services/customer-service';
import { RoomService } from '../../services/room-service';
import { Booking } from '../../models/booking.model';
import { Room } from '../../models/room.model';
import { Customer } from '../../models/customer.model';

@Component({
  selector: 'app-add-update-booking-dialog',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatSelectModule,
    MatButtonModule,
    MatDialogTitle,
    MatDialogActions,
    MatDatepickerModule,
    ReactiveFormsModule,
    CommonModule,
    MatProgressBarModule,
    MatDialogContent,
    MatAutocompleteModule,
    AsyncPipe,
  ],
  providers: [provideNativeDateAdapter()],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './add-update-booking-dialog.html',
  styleUrl: './add-update-booking-dialog.scss',
})
export class AddUpdateBookingDialog implements OnInit {
  form!: FormGroup;
  customerControll = new FormControl<Customer | any>('');
  roomControll = new FormControl<Customer | any>('');
  booking: Booking = inject<Booking>(MAT_DIALOG_DATA);

  availableRooms: Room[] = [];
  customersLit: Customer[] = [];

  filteredAvailabaleRooms!: Observable<Room[]>;
  filteredCustomers!: Observable<Customer[]>;

  constructor(
    private fb: FormBuilder,
    private _cutomerService: CustomerService,
    private _roomService: RoomService,
    public _dialogRef: MatDialogRef<AddUpdateBookingDialog>
  ) {}

  ngOnInit(): void {
    combineLatest([
      this._roomService.getRooms(),
      this._cutomerService.getAll(),
    ]).subscribe(([rooms, customers]) => {
      this.availableRooms = rooms.items.filter((r) => r.isAvailable == true);
      this.customersLit = customers.items;
    });

    this.filteredCustomers = this.customerControll.valueChanges.pipe(
      startWith(''),
      map((value) => this._filterCustomer(value || ''))
    );

    this.filteredAvailabaleRooms = this.roomControll.valueChanges.pipe(
      startWith(''),
      map((value) => this._filterRoom(value || ''))
    );

    this.initForm();
  }

  initForm() {
    if (this.booking.id) {
      this.form = this.fb.group({
        notes: [this.booking.notes, [Validators.minLength(15)]],
        checkInDate: [this.booking.checkInDate, [Validators.required]],
        checkOutDate: [this.booking.checkOutDate, [Validators.required]],
        totalPrice: [this.booking.totalPrice],
      });
    } else {
      this.form = this.fb.group({
        notes: ['', [Validators.minLength(15)]],
        customer: [],
        room: [],
        checkInDate: [new Date(), [Validators.required]],
        checkOutDate: [new Date(), [Validators.required]],
        totalPrice: [0],
      });
    }
  }

  calculateTotal(startDate: Date, endDate: Date): number {
    return 1;
  }

  private _filterCustomer(value: string): Customer[] {
    return this.customersLit.filter(
      (c) =>
        c.firstName.toLowerCase().includes(value) ||
        c.lastName.toLowerCase().includes(value)
    );
  }

  displayCustomer(customer: Customer): string {
    return customer && customer.firstName
      ? customer.firstName + ' ' + customer.lastName
      : '';
  }

  private _filterRoom(value: string): Room[] {
    return this.availableRooms.filter((r) => r.number.includes(value));
  }

  displayRoom(room: Room): string {
    return room && room.number
      ? room.number + ' for ' + room.capacity + ' peopple'
      : '';
  }

  onSubmit() {
    if (this.form.valid) {
      (this.booking.notes = this.form.value.notes),
        (this.booking.customer = this.customerControll.value);
      this.booking.room = this.roomControll.value;
      this.booking.checkInDate = this.form.value.checkInDate;
      this.booking.checkOutDate = this.form.value.checkOutDate;
      this.booking.status = 'Pending';
      this.booking.totalPrice =
        this.booking.room.pricePerNight *
        this.differenceInDays(
          this.form.value.checkInDate,
          this.form.value.checkOutDate
        );

      this._dialogRef.close(this.booking);
    } else {
      console.log('error: ', this.form.errors);
    }
  }

  differenceInDays(date1: Date, date2: Date): number {
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    const diffInTime = date2.getTime() - date1.getTime();
    return Math.round(diffInTime / oneDay);
  }

  // toCSharpDate = (date: Date): string => {
  //   const year = date.getFullYear();
  //   const month = (date.getMonth() + 1).toString().padStart(2, '0');
  //   const day = date.getDate().toString().padStart(2, '0');
  //   return `${year}-${month}-${day}`;
  // };

  get formControls() {
    return this.form.controls;
  }
}

// this.booking.customer = {
//   id = this.customerControll.value.id,
//   firstName = this.customerControll.value.firstName,
//   lastName = this.customerControll.value.lastName,
//   email = this.customerControll.value.email,
//   phoneNumber = this.customerControll.value.phoneNumber,
//   country = this.customerControll.value.country
// },
