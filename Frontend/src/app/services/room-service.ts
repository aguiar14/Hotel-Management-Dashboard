import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Room } from '../models/room.model';
import { ApiResponse } from '../models/apiresponse.model';
import { RoomsFilter } from '../pages/room/rooms.filter';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  private readonly apiUrl = '/api/rooms';

  constructor(private httpclient: HttpClient) {}

  getAll(filter: RoomsFilter): Observable<ApiResponse<Room>> {
    return this.httpclient.get<ApiResponse<Room>>(this.apiUrl, {
      params: {
        roomType: filter.type,
        isAvailable: filter.status,
        capacity: filter.capacity,
      },
    });
  }

  add(room: Room): Observable<any> {
    return this.httpclient.post(this.apiUrl, room);
  }

  edit(id: number, room: Room): Observable<any> {
    return this.httpclient.put(this.apiUrl + '/' + id, room);
  }

  getById(id: number): Observable<any> {
    return this.httpclient.get(this.apiUrl + '/' + id);
  }

  delete(id: number): Observable<any> {
    return this.httpclient.delete(this.apiUrl + '/' + id);
  }
}
