import { Customer } from "./customer";
import { ServerResponse } from "./server-error";

export interface ListCustomerRequest {
    id?: string;
    name?: string;
    limit: number;
    start: Date;
    end: Date
    paginationToken?: string
}

export interface ListCustomerResponse extends ServerResponse<DataResponse<Customer>> {
}

export interface DataResponse<T> {
     items:T[],
     hasMore: boolean;
     lastKey?: string;
} 
