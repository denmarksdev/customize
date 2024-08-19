import { Customer } from "./customer";

export interface ListCustomerRequest {
    id?: string;
    name?: string;
    limit: number;
    start: Date;
    end: Date
    paginationToken?: string
}

export interface ListCustomerResponse {
    data: DataResponse<Customer>;
    message?: string;
    success:boolean;
    errors?: Map<string, string>;
}

export interface DataResponse<T> {
     items:T[],
     hasMore: boolean;
     lastKey?: string;
} 
