import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { PageTitleComponent } from '../page-title/page-title.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [RouterModule,FormsModule, ReactiveFormsModule, MatSelectModule, MatInputModule, MatFormFieldModule, MatButtonModule, MatProgressBarModule, PageTitleComponent],
  templateUrl: './customer-form.component.html',
  styleUrl: './customer-form.component.scss',
})
export class CustomerFormComponent implements OnInit {

  customerForm: FormGroup
  isLoading: boolean = false

  constructor(private formBuilder: FormBuilder) {
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
      const newCustomer = this.customerForm.value;
      console.log('Customer registered: ', newCustomer);
      // Aqui você pode adicionar a lógica para enviar o cliente para um servidor/back-end
    }
  }

  get name() {
    return this.customerForm.get('name');
  }
  get email() {
    return this.customerForm.get('email');
  }
  get cellphone() {
    return this.customerForm.get('cellphone');
  }
}
