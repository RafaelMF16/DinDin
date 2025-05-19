import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { hoverCardTrigger } from '../../animations';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';

@Component({
  selector: 'app-monthly-summary-card',
  standalone: false,
  templateUrl: './monthly-summary-card.component.html',
  styleUrl: './monthly-summary-card.component.css',
  animations: [
    hoverCardTrigger
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
    this.cardClick.emit(this.monthlySummary.id);
  }
}
