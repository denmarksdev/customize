import { Component } from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import { Customer, CustomerView } from '../../model/customer';
import { PageTitleComponent } from '../page-title/page-title.component';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { RouterModule } from '@angular/router';


const ELEMENT_DATA: CustomerView[] = [
  {id: '1', name: 'Denis',   cellphone: '5511999995555', email: 'teste@teste.com', createdAt: new Date(2024,8,18), action:''},
  {id: '2', name: 'Maria',   cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '3', name: 'João',    cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '4', name: 'Pedro',   cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '5', name: 'Paulo',   cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '6', name: 'Tiago',   cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '7', name: 'Fernanda',cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '8', name: 'Amanda',  cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '9', name: 'Paulo',   cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
  {id: '10', name: 'Bruna',  cellphone: '5511999995555', email: 'teste@teste.com',createdAt: new Date(2024,8,18), action:''},
];

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [RouterModule,MatTableModule, PageTitleComponent, MatButtonModule, MatIconModule],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.scss'
})
export class CustomerListComponent {
  displayedColumns: string[] = ['id', 'name', 'email', 'cellphone', 'createdAt', 'action'];
  dataSource = ELEMENT_DATA;
  clickedRows = new Set<Customer>();
}
