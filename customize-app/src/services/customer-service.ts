import { Injectable } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { ListCustomerRequest, ListCustomerResponse } from '../model/customer-query';
import { environment } from '../environments/enviroment';
import { BaseCustomer, Customer } from '../model/customer';
import { catchError, Observable, throwError } from 'rxjs';
import { ServerError } from '../model/server-error';

const pipe = new DatePipe('en-US');

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  save(newCustomer: BaseCustomer) {
    return this.http.post(environment.baseurl + `v1/customers`, newCustomer)
      .pipe(
        catchError(this.handleError)
      );
  }

  customers: Customer[]

  constructor(private http: HttpClient) {
    this.customers = [];
  }

  list(query: ListCustomerRequest): Observable<ListCustomerResponse> {

    const params = new HttpParams()
      .set('limit', query.limit)
      .set('start', pipe.transform(query.start, 'yyyy-MM-dd') ?? '')
      .set('end', pipe.transform(query.end, 'yyyy-MM-dd') ?? '')

    return this.http.get<ListCustomerResponse>(environment.baseurl + `v1/customers`, { params, responseType: 'json' })
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error) {
      var serverError = error.error as ServerError
      if (serverError) {
        return throwError(() => serverError);
      }
    }
    var errors = new Map();
    errors.set('Erro não gerênciado', error.message);

    serverError = {
      errors,
      message : error.message,
      success: false
    }

    return throwError(() => serverError);
  }
}
