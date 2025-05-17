import { DatePipe } from '@angular/common';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FormatterService {

  private datePipe = inject(DatePipe);

  private monthList: string[] = [
    'Janeiro',
    'Fevereiro',
    'Março',
    'Abril',
    'Maio',
    'Junho',
    'Julho',
    'Agosto',
    'Setembro',
    'Outubro',
    'Novembro',
    'Dezembro',
  ];

  formatteMonth(month: number): string {
    const indexCorrection = 1;
    return this.monthList[month - indexCorrection];
  }

  formatteDate(date: string): string {
    const datePattern = "dd/MM/yyyy";
    return this.datePipe.transform(date, datePattern)!;
  }
}
