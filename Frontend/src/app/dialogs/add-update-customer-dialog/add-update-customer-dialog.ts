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
import { Customer } from '../../models/customer.model';
import { CustomerService } from '../../services/customer-service';

@Component({
  selector: 'app-add-update-customer-dialog',
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
  templateUrl: './add-update-customer-dialog.html',
  styleUrl: './add-update-customer-dialog.scss',
})
export class AddUpdateCustomerDialog implements OnInit {
  form!: FormGroup;
  customer: Customer = inject<Customer>(MAT_DIALOG_DATA);

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<AddUpdateCustomerDialog>,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm() {
    if (this.customer.id) {
      this.form = this.fb.group({
        firstName: [
          this.customer.firstName,
          [Validators.required, Validators.minLength(2)],
        ],
        lastName: [
          this.customer.lastName,
          [Validators.required, Validators.minLength(3)],
        ],
        email: [this.customer.email, [Validators.required, Validators.email]],
        phoneNumber: [
          this.customer.phoneNumber,
          [Validators.required, Validators.minLength(7)],
        ],
        country: [this.customer.country, [Validators.minLength(3)]],
      });
    } else {
      this.form = this.fb.group({
        firstName: ['', [Validators.required, Validators.minLength(2)]],
        lastName: ['', [Validators.required, Validators.minLength(3)]],
        email: ['', [Validators.required, Validators.email]],
        phoneNumber: ['', [Validators.required, Validators.minLength(7)]],
        country: ['', [Validators.minLength(3)]],
      });
    }
  }

  onSubmit() {
    if (this.form.valid) {
      (this.customer.firstName = this.form.value.firstName),
        (this.customer.lastName = this.form.value.lastName),
        (this.customer.email = this.form.value.email),
        (this.customer.phoneNumber = this.form.value.phoneNumber),
        (this.customer.country = this.form.value.country),
        this.dialogRef.close(this.customer);
    }
  }

  get formControls() {
    return this.form.controls;
  }
}
