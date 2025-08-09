import { DataSource } from '@angular/cdk/collections';
import { Customer } from '../../models/customer.model';
import { delay, Observable, ReplaySubject } from 'rxjs';
import { CustomerService } from '../../services/customer-service';
import { DialogService } from '../../services/dialog-service';
import { ApiResponse } from '../../models/apiresponse.model';
import { error } from 'console';

export class CustomerDataSource extends DataSource<Customer> {
  private _dataStream = new ReplaySubject<Customer[]>();
  private total = new ReplaySubject<number>();

  constructor(
    private _customerService: CustomerService,
    private dialogService: DialogService
  ) {
    super();
  }

  connect(): Observable<Customer[]> {
    return this._dataStream;
  }

  disconnect() {
    this._dataStream.complete();
    this.total.complete();
  }

  loadCustomers() {
    this.dialogService.showLoading();
    this._customerService
      .getAll()
      .pipe(delay(300))
      .subscribe(
        (response: ApiResponse<Customer>) => {
          this.setData(response.items);
          console.log(response.items);
          this.dialogService.hideLoading();
        },
        (error) => {
          console.error('error loading customer', error);
          this.setData([]);
        }
      );
  }

  private setData(data: Customer[]) {
    this._dataStream.next(data);
    this.total.next(data.length);
  }
}
