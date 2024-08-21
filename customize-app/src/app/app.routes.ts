import { Routes } from '@angular/router';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomerFormComponent } from './customer-form/customer-form.component';

export const routes: Routes = [
    { path: '', component: CustomerListComponent },
    { path: 'new-customer', component: CustomerFormComponent },
    { path: 'edit-customer/:id', component: CustomerFormComponent }
];
