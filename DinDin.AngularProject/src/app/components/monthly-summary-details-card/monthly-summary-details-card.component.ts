import { Component, computed, inject, input } from '@angular/core';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { MatCard, MatCardHeader, MatCardTitle, MatCardSubtitle, MatCardContent } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { NgClass, CurrencyPipe, CommonModule } from '@angular/common';
import { I18nService } from '../../services/i18nService/i18n.service';

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
    CurrencyPipe,
    CommonModule
  ]
})
export class MonthlySummaryDetailsCardComponent {

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
}
