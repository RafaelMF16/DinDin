import { Component, inject, Input } from '@angular/core';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { MatCard, MatCardHeader, MatCardTitle, MatCardSubtitle, MatCardContent } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { NgClass, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-monthly-summary-details-card',
  templateUrl: './monthly-summary-details-card.component.html',
  styleUrl: './monthly-summary-details-card.component.css',
  standalone: true,
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitle,
    MatCardSubtitle,
    MatCardContent,
    MatIcon,
    NgClass, 
    CurrencyPipe
  ]
})
export class MonthlySummaryDetailsCardComponent {

  private formatterService = inject(FormatterService);

  _monthName!: string;

  @Input() monthlySummary?: MonthlySummary
  @Input() set month(value: number) {
    this._monthName = this.formatterService.formatteMonth(value);
  }
}
