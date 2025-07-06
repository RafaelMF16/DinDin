import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { hoverCardTrigger } from '../../animations';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { MatCard, MatCardHeader, MatCardTitle, MatCardContent } from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';
import { NgClass, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-monthly-summary-card',
  templateUrl: './monthly-summary-card.component.html',
  styleUrl: './monthly-summary-card.component.css',
  standalone: true,
  animations: [
    hoverCardTrigger
  ],
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardContent,
    MatDivider,
    NgClass,
    CurrencyPipe
  ]
})
export class MonthlySummaryCardComponent implements OnInit {
  @Input() monthlySummary!: MonthlySummary;

  @Output() cardClick = new EventEmitter<string>();

  private formatterService = inject(FormatterService);

  hoverState: string = 'neutral';
  monthName: string = '';

  ngOnInit(): void {
    this.monthName = this.formatterService.formatteMonth(this.monthlySummary.month);
  }

  onClickInCard(): void {
    this.cardClick.emit(this.monthlySummary.id.toString());
  }
}
