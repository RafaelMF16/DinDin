import { Component, Input } from '@angular/core';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';

@Component({
  selector: 'app-monthly-summary-details-card',
  standalone: false,
  templateUrl: './monthly-summary-details-card.component.html',
  styleUrl: './monthly-summary-details-card.component.css'
})
export class MonthlySummaryDetailsCardComponent {

  @Input() monthlySummary!: MonthlySummary

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
