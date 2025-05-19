import { Component, inject, Input, OnInit } from '@angular/core';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';

@Component({
  selector: 'app-monthly-summary-details-card',
  standalone: false,
  templateUrl: './monthly-summary-details-card.component.html',
  styleUrl: './monthly-summary-details-card.component.css'
})
export class MonthlySummaryDetailsCardComponent {

  private formatterService = inject(FormatterService);

  _monthName!: string;

  @Input() monthlySummary?: MonthlySummary
  @Input() set month(value: number) {
    this._monthName = this.formatterService.formatteMonth(value);
  }
}
