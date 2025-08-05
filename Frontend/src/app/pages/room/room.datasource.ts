import { DataSource } from '@angular/cdk/collections';
import { Observable, ReplaySubject } from 'rxjs';
import { RoomService } from '../../services/room-service';
import { Room } from '../../models/room.model';
import { ApiResponse } from '../../models/apiresponse.model';

export class RoomDataSource extends DataSource<Room> {
  private _dataStream = new ReplaySubject<Room[]>();
  public total = new ReplaySubject<number>();

  constructor(private _roomService: RoomService) {
    super();
  }

  connect(): Observable<Room[]> {
    return this._dataStream;
  }

  disconnect() {
    this._dataStream.complete();
    this.total.complete();
  }

  loadRooms() {
    this._roomService.getAll().subscribe(
      (response: ApiResponse<Room>) => {
        this.setData(response.items);
        console.log(response);
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
