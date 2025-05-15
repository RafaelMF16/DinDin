import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-monthly-summary-details-card',
  standalone: false,
  templateUrl: './monthly-summary-details-card.component.html',
  styleUrl: './monthly-summary-details-card.component.css'
})
export class MonthlySummaryDetailsCardComponent {

  @Input() year!: number;
  @Input() totalIncome!: number;
  @Input() totalExpense!: number;
  @Input() balance!: number;

  @Input()
  set month(value: number) {
    this._month = value;
    const indexCorrection = 1;
    this.monthName = this.monthList[value - indexCorrection];
  }

  private _month!: number;
  monthName: string = '';

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
}
