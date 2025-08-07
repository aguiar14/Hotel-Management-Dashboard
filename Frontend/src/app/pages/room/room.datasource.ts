import { DataSource } from '@angular/cdk/collections';
import { delay, Observable, ReplaySubject } from 'rxjs';
import { RoomService } from '../../services/room-service';
import { Room } from '../../models/room.model';
import { ApiResponse } from '../../models/apiresponse.model';
import { DialogService } from '../../services/dialog-service';
import { RoomsFilter } from './rooms.filter';

export class RoomDataSource extends DataSource<Room> {
  private _dataStream = new ReplaySubject<Room[]>();
  public total = new ReplaySubject<number>();

  constructor(
    private _roomService: RoomService,
    private dialogService: DialogService,
    private filter: RoomsFilter
  ) {
    super();
    filter.filterChanged.subscribe(() => {
      this.loadRooms();
    });
  }

  connect(): Observable<Room[]> {
    return this._dataStream;
  }

  disconnect() {
    this._dataStream.complete();
    this.total.complete();
  }

  loadRooms() {
    this.dialogService.showLoading();
    this._roomService
      .getAll(this.filter)
      .pipe(delay(300))
      .subscribe(
        (response: ApiResponse<Room>) => {
          this.setData(response.items);
          this.dialogService.hideLoading();
        },
        (error) => {
          console.error('erro geting rooms', error);
          this.setData([]);
        }
      );
  }

  private setData(data: Room[]) {
    this._dataStream.next(data);
    this.total.next(data.length);
  }
}
