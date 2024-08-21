import { Component, Injectable, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { PageTitleComponent } from '../page-title/page-title.component';
import { RouterModule } from '@angular/router';
import { CustomerService } from '../../services/customer-service';
import { BaseCustomer, Customer, } from '../../model/customer';
import { catchError, throwError } from 'rxjs';
import { ServerError } from '../../model/server-error';
import { NgFor, KeyValuePipe, DatePipe } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule, MatSelectModule, MatInputModule, MatFormFieldModule, MatButtonModule, MatProgressBarModule, PageTitleComponent, NgFor, KeyValuePipe, DatePipe],
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.scss',
})

@Injectable()
export class CustomerFormComponent implements OnInit {

  customerForm: FormGroup
  isLoading: boolean = false
  customer?: Customer
  hasCustomer?: boolean = false;
  title?: string
  action?: string

  serverError?: ServerError

  constructor(private formBuilder: FormBuilder, private customerService: CustomerService, private router: Router, private activeRoute: ActivatedRoute) {
    this.customerForm = this.formBuilder.group({
      name: ['', Validators.required],
      cellphone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  ngOnInit(): void {

    this.activeRoute.params.subscribe(param => {
      this.customer = undefined;

      const customerId = param['id'];
      if (!customerId) {
        this.title = "Cadastrar cliente"
        this.action = "Cadastrar"
        return;
      }

      this.isLoading = true;
      this.title = "Atualizar cliente"
      this.action = "Atualizar"

      this.customerService.find(customerId)
        .pipe(
          catchError((error: ServerError) => {
            console.log('Find customer error', error.errors)
            this.serverError = error
            this.isLoading = false;
            return throwError(() => error);
          })
        )
        .subscribe(reponse => {
          this.hasCustomer = true;
          this.customer = reponse.data;
          this.customerForm.setValue(
            {
              'name': this.customer.name,
              'cellphone': this.customer.cellphone,
              'email': this.customer.email,
            })
        }
        );
      this.isLoading = false;
    })
  }

  onSubmit() {
    if (this.customerForm.valid) {

      this.isLoading = true;

      if (this.serverError?.errors) {
        this.serverError.errors = new Map<string, string>();
      }
      

      if (this.customer === undefined ) {
        const newCustomer = this.customerForm.value as BaseCustomer;

        this.customerService.save(newCustomer)
          .pipe(
            catchError((error: ServerError) => {
              console.log('Server error save customer', error.errors)
              this.serverError = error
              this.isLoading = false;
              return ''
            })
          )
          .subscribe(r => {
            this.isLoading = false;
            this.router.navigate(['/']);
          });
          return;
      }

      const updateCustomer = this.customerForm.value as Customer;
      updateCustomer.id = this.customer.id;
      updateCustomer.createdAt = this.customer.createdAt;

      this.customerService.update(updateCustomer)
          .pipe(
            catchError((error: ServerError) => {
              console.log('Server error update customer', error.errors)
              this.serverError = error
              this.isLoading = false;
              return ''
            })
          )
          .subscribe(r => {
            this.isLoading = false;
            this.router.navigate(['/']);
          });
      
    }
  }

  public mapError(key: string, value: string) {
    switch (key) {
      case 'email': return 'Email inválido'
      case 'required': return 'Campo obrigatório'
      case 'Cellphone': return value
      default:
        return key
    }
  }

  get name() {
    return this.customerForm.get('name');
  }
  get email() {
    var email = this.customerForm.get('email');
    return email
  }
  get cellphone() {
    return this.customerForm.get('cellphone');
  }
}
