import { Injectable } from '@angular/core';
import { DatePipe} from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ListCustomerRequest, ListCustomerResponse } from '../model/customer-query';
import { environment } from '../environments/enviroment';
import { Customer } from '../model/customer';
import { Observable } from 'rxjs';

const pipe = new DatePipe('en-US');

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  customers: Customer[]

  constructor(private http: HttpClient) {
    this.customers = [];
  }

  list(query: ListCustomerRequest): Observable<ListCustomerResponse> {

    const params = new HttpParams()
      .set('limit', query.limit)
      .set('start', pipe.transform(query.start, 'yyyy-MM-dd')?? '')
      .set('end', pipe.transform(query.end, 'yyyy-MM-dd')?? '')

    console.log(JSON.stringify(params))
    return this.http.get<ListCustomerResponse>(environment.baseurl + `v1/customers`, { params, responseType:'json' });
  }
}
