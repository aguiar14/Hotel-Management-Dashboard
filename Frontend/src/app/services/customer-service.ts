import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/apiresponse.model';
import { Customer } from '../models/customer.model';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private readonly apiUrl = '/api/customers';

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<ApiResponse<Customer>> {
    return this.httpClient.get<ApiResponse<Customer>>(this.apiUrl);
  }

  add(customer: Customer): Observable<any> {
    return this.httpClient.post(this.apiUrl, customer);
  }

  edit(id: number, customer: Customer): Observable<any> {
    return this.httpClient.put(this.apiUrl + '/' + id, customer);
  }

  getById(id: number): Observable<any> {
    return this.httpClient.get(this.apiUrl + '/' + id);
  }

  delete(id: number): Observable<any> {
    return this.httpClient.delete(this.apiUrl + '/' + id);
  }
}
