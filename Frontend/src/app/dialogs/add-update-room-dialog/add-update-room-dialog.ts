import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { map } from 'rxjs';
import { MatProgressBarModule } from '@angular/material/progress-bar';
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
import { Room } from '../../models/room.model';
import { RoomType } from '../../models/roomtype.model';
import { RoomTypeService } from '../../services/room-type-service';

@Component({
  selector: 'app-add-update-room-dialog',
  providers: [provideNativeDateAdapter()],
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
  ],
  templateUrl: './add-update-room-dialog.html',
  styleUrl: './add-update-room-dialog.scss',
})
export class AddUpdateRoomDialog implements OnInit {
  form!: FormGroup;
  room: Room = inject<Room>(MAT_DIALOG_DATA);
  roomTypes: RoomType[] = [];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddUpdateRoomDialog>,
    private roomTypeService: RoomTypeService
  ) {}

  ngOnInit(): void {
    this.roomTypeService.getAll().subscribe((response) => {
      this.roomTypes = response.items;
    });

    this.initForm();
  }

  initForm() {
    if (this.room.id) {
      this.form = this.fb.group({
        number: [this.room.number, [Validators.required]],
        description: [this.room.description, [Validators.required]],
        capacity: [
          this.room.capacity,
          [Validators.required, Validators.min(1)],
        ],
        pricePerNight: [
          this.room.pricePerNight,
          [Validators.required, Validators.min(50)],
        ],
        isAvailable: [this.room.isAvailable, [Validators.required]],
        roomType: [this.room.roomType.id, [Validators.required]],
      });
    } else {
      this.form = this.fb.group({
        number: ['', [Validators.required]],
        description: ['', [Validators.required]],
        capacity: [1, [Validators.required, Validators.min(1)]],
        pricePerNight: [50, [Validators.required, Validators.min(50)]],
        isAvailable: [true, [Validators.required]],
        roomType: [this.roomTypes[0]?.id, [Validators.required]],
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      this.room.number = this.form.value.number;
      this.room.description = this.form.value.description;
      this.room.capacity = this.form.value.capacity;
      this.room.pricePerNight = this.form.value.pricePerNight;
      this.room.isAvailable = this.form.value.isAvailable;
      this.room.roomType = this.roomTypes.find(
        (rt) => rt.id === this.form.value.roomType
      )!;
      this.dialogRef.close(this.room);
    }
  }

  get formControls() {
    return this.form.controls;
  }
}
