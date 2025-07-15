import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FormatterService {

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
}
