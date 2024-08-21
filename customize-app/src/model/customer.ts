export interface BaseCustomer {
    name: string,
    email:string,
    cellphone:string
}

export interface Customer extends BaseCustomer  {
    id: string,
    createdAt: Date
}

export interface CustomerView extends Customer  {
    action: string  
}
