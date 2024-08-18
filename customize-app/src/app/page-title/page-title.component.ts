import { Component, Input } from '@angular/core';
import {MatDividerModule} from '@angular/material/divider';

@Component({
  selector: 'app-page-title',
  standalone: true,
  imports: [MatDividerModule],
  templateUrl: './page-title.component.html',
  styleUrl: './page-title.component.scss'
})
export class PageTitleComponent {

  @Input({
    required:true
  })
  title: string;

  constructor(){
    this.title = 'Titulo'
  }
}
