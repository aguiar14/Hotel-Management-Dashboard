import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Booking } from '../models/booking.model';
import { ApiResponse } from '../models/apiresponse.model';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private readonly apiUrl: string = '/api/booking';

  constructor(private httpClient: HttpClient) {}

  gettAll(): Observable<ApiResponse<Booking>> {
    return this.httpClient.get<ApiResponse<Booking>>(this.apiUrl, {
      params: {
        roomId: -1,
        customerId: -1,
        status: '',
      },
    });
  }

  getById(id: number): Observable<any> {
    return this.httpClient.get(this.apiUrl + '/' + id);
  }

  add(booking: Booking): Observable<any> {
    return this.httpClient.post(this.apiUrl, booking);
  }

  edit(id: number, booking: Booking): Observable<any> {
    return this.httpClient.put(this.apiUrl + '/' + id, booking);
  }

  confirm(id: number, booking: Booking): Observable<any> {
    return this.httpClient.put(this.apiUrl + '/' + id + '/confirm', booking);
  }

  cancel(id: number, booking: Booking): Observable<any> {
    return this.httpClient.put(this.apiUrl + '/' + id + '/cancel', booking);
  }
}
