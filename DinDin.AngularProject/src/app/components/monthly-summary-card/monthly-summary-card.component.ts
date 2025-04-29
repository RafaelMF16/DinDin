import { Component, Input, OnInit } from '@angular/core';
import { hoverCardTrigger } from '../../animations';

@Component({
  selector: 'app-monthly-summary-card',
  standalone: false,
  templateUrl: './monthly-summary-card.component.html',
  styleUrl: './monthly-summary-card.component.css',
  animations: [
    hoverCardTrigger
  ]
})
export class MonthlySummaryCardComponent implements OnInit{
  @Input() month!: number;
  @Input() totalIncome!: number;
  @Input() totalExpense!: number;
  @Input() balance!: number;

  hoverState: string = 'neutral';
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
  ]

  ngOnInit(): void {
    this.getMonthName();
  }

  getMonthName(): void {
    const listIndexCorrection: number = 1;
    this.monthName = this.monthList[this.month - listIndexCorrection]
  }
}
