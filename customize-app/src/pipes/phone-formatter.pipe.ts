import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'phoneFormatter', standalone:true})
export class PhoneFormatterPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return value;


    let formattedPhone = value.replace(/(\d{2})(\d{2})(\d{4})(\d{4})/, '+$1 ($2) $3-$4');
    return formattedPhone;
  }
}
