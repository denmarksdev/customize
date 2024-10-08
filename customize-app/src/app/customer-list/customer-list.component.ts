import { Component, OnInit, Injectable, inject } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { Customer } from '../../model/customer';
import { PageTitleComponent } from '../page-title/page-title.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { CustomerService } from '../../services/customer-service';
import { ListCustomerRequest } from '../../model/customer-query';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common'
import { DialogData, ModalComponent } from '../modal/modal.component';

// TODO: refatorar componente dedicado.
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { provideNativeDateAdapter } from '@angular/material/core';
import { PhoneFormatterPipe } from '../../pipes/phone-formatter.pipe';
import { MatDialog } from '@angular/material/dialog';
import { ServerError } from '../../model/server-error';
import { catchError, throwError } from 'rxjs';


@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [
    RouterModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatProgressBarModule,
    PageTitleComponent,
    DatePipe,
    PhoneFormatterPipe
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.scss'
})


@Injectable()
export class CustomerListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'email', 'cellphone', 'createdAt', 'action'];
  dataSource: Customer[];
  query: ListCustomerRequest
  isLoading: boolean = false;

  readonly range = new FormGroup({
    start: new FormControl<Date | null>(new Date()),
    end: new FormControl<Date | null>(new Date()),
  });

  readonly dialog = inject(MatDialog);

  constructor(private customerService: CustomerService) {
    this.dataSource = [],
      this.query = {
        start: new Date(),
        end: new Date(),
        limit: 1,
        name: '',
        id: ''
      }
  }

  openDialog(id: string): void {
    const data: DialogData = {
      title: "Excluir cliente",
      message: "Confirma a exclusão?"
    }

    const dialogRef = this.dialog.open(ModalComponent, { data });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.isLoading = true
        this.customerService.delete(id).pipe(
          catchError((error: ServerError) => {
            console.log('Find customer error', error.errors)
            this.isLoading = false
            return throwError(() => error);
          })
        )
          .subscribe(_ => {
            this.ngOnInit();
          }
          
        );
      }
    });

  }

  onSubmit() {
    this.isLoading = true;
    if (this.range.valid) {

      var start = this.range.get('start')?.value
      var end = this.range.get('end')?.value

      this.query.start = start as Date;
      this.query.end = end as Date;

      this.customerService.list(this.query).subscribe(response => {
        if (response.success) {
          this.isLoading = false;
          this.dataSource = response.data.items
        }
      })
    }
  }

  ngOnInit(): void {
    this.isLoading = true;
    if (this.range.valid) {

      var start = this.range.get('start')?.value
      var end = this.range.get('end')?.value

      this.query.start = start as Date;
      this.query.end = end as Date;

      this.customerService.list(this.query).subscribe(response => {
        if (response.success) {
          this.isLoading = false;
          this.dataSource = response.data.items
        }
      })
    }
  }
}
