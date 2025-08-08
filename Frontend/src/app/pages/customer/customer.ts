import { Component, inject, OnInit } from '@angular/core';
import { Customer } from '../../models/customer.model';

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
import { CustomerDataSource } from './customer.datasource';
import { CustomerService } from '../../services/customer-service';
import { AddUpdateCustomerDialog } from '../../dialogs/add-update-customer-dialog/add-update-customer-dialog';

@Component({
  selector: 'app-customer',
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
  templateUrl: './customer.html',
  styleUrl: './customer.scss',
})
export class CustomerComponent implements OnInit {
  data!: CustomerDataSource;

  resultsLength: number = 3;
  displayedColumns: string[] = [
    'firstname',
    'lastname',
    'email',
    'phonenumber',
    'country',
    'actions',
  ];

  constructor(
    private _customerService: CustomerService,
    private dialogService: DialogService
  ) {
    this.data = new CustomerDataSource(_customerService, dialogService);
  }

  ngOnInit(): void {
    this.data.loadCustomers();
    console.log(this.data);
  }

  add() {
    var customer = {
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      country: '',
    };

    const dialogRef = this.dialogService.open(AddUpdateCustomerDialog, {
      data: customer,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this._customerService
          .add(result)
          .subscribe(() => this.data.loadCustomers());
      }
    });
  }

  edit(customerEdit: Customer) {
    const dialogRef = this.dialogService.open(AddUpdateCustomerDialog, {
      data: customerEdit,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result !== null && result != undefined) {
        this._customerService.edit(customerEdit.id, result).subscribe(() => {
          this.data.loadCustomers();
        });
      }
    });
  }
  deleteCustomer(id: number) {
    throw new Error('Method not implemented.');
  }
}
