import { Component, EventEmitter, Injectable, OnInit, Output, output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { PageTitleComponent } from '../page-title/page-title.component';
import { RouterModule } from '@angular/router';
import { CustomerService } from '../../services/customer-service';
import { BaseCustomer, } from '../../model/customer';
import { catchError } from 'rxjs';
import { ServerError } from '../../model/server-error';
import { NgFor, KeyValuePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule, MatSelectModule, MatInputModule, MatFormFieldModule, MatButtonModule, MatProgressBarModule, PageTitleComponent, NgFor, KeyValuePipe],
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.scss',
})

@Injectable()
export class CustomerFormComponent implements OnInit {

  customerForm: FormGroup
  isLoading: boolean = false

  serverError?: ServerError

  constructor(private formBuilder: FormBuilder, private customerService: CustomerService, private router: Router) {
    this.customerForm = this.formBuilder.group({
      name: ['', Validators.required],
      cellphone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  ngOnInit(): void { }

  onSubmit() {
    if (this.customerForm.valid) {

      this.isLoading = true;

      if (this.serverError?.errors) {
        this.serverError.errors = new Map<string, string>();
      }
      const newCustomer = this.customerForm.value as BaseCustomer;

      this.customerService.save(newCustomer)
        .pipe(
          catchError((error: ServerError) => {

            console.log('Server error', error.errors)
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
