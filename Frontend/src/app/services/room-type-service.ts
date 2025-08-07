import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../models/apiresponse.model';
import { RoomType } from '../models/roomtype.model';

@Injectable({
  providedIn: 'root',
})
export class RoomTypeService {
  private readonly apiUrl = '/api/roomtype';

  constructor(private httpclient: HttpClient) {}

  getAll() {
    return this.httpclient.get<ApiResponse<RoomType>>(this.apiUrl);
  }
}
