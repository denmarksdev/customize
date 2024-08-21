import { Component, inject, Input} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatButtonModule} from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogContent,
  MatDialogRef,
} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';

export interface DialogData {
  title: string
  message:string
}

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, FormsModule, MatButtonModule, MatDialogContent, MatDialogActions ],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent {
  readonly dialogRef = inject(MatDialogRef<ModalComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);

  public onNoClick(){
    this.dialogRef.close();
  }

  public onOk(){
    this.dialogRef.close(true)
  }
}