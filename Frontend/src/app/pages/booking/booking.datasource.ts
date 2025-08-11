import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { delay, Observable, ReplaySubject } from 'rxjs';
import { Booking } from '../../models/booking.model';
import { BookingService } from '../../services/booking-service';
import { DialogService } from '../../services/dialog-service';
import { ApiResponse } from '../../models/apiresponse.model';

export class BookingDataSource extends DataSource<Booking> {
  private _dataStream = new ReplaySubject<Booking[]>();
  public total = new ReplaySubject<number>();

  constructor(
    private _bookingService: BookingService,
    private dialogService: DialogService
  ) {
    super();
  }

  connect(): Observable<readonly Booking[]> {
    return this._dataStream;
  }
  disconnect(): void {
    this._dataStream.complete();
    this.total.complete();
  }

  loadBookings() {
    this.dialogService.showLoading();
    this._bookingService
      .gettAll()
      .pipe(delay(300))
      .subscribe(
        (response: ApiResponse<Booking>) => {
          console.log(response.items);
          this.setData(response.items);
          this.dialogService.hideLoading();
        },
        (error) => {
          console.error('error', error);
          this.setData([]);
        }
      );
  }

  setData(data: Booking[]) {
    this._dataStream.next(data);
    this.total.next(data.length);
  }
}
