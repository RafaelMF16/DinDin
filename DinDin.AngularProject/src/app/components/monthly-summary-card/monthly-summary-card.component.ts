import { Component, computed, effect, EventEmitter, inject, input, Output, signal } from '@angular/core';
import { hoverCardTrigger } from '../../animations';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { MatCard, MatCardHeader, MatCardTitle, MatCardContent } from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';
import { NgClass, CurrencyPipe, CommonModule } from '@angular/common';
import { I18nService } from '../../services/i18nService/i18n.service';

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
    CurrencyPipe,
    CommonModule
  ]
})
export class MonthlySummaryCardComponent{

  readonly monthlySummary = input<MonthlySummary>();
  
  private readonly formatterService = inject(FormatterService);
  private readonly i18nService = inject(I18nService);

  readonly currencyCode = computed(() => {
    return this.i18nService.getCurrencyCode();
  });
  
  readonly monthName = computed(() => {
    const monthlySummary = this.monthlySummary();
    return monthlySummary 
      ? this.formatterService.formatteMonth(monthlySummary.month)
      : "";
  });

  @Output() cardClick = new EventEmitter<string>();

  hoverState: string = 'neutral';

  onClickInCard(): void {
    this.cardClick.emit(this.monthlySummary()?.id.toString());
  }
}
