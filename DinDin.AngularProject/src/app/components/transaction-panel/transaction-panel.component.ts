import { Component, computed, effect, inject, input, signal } from '@angular/core';
import { Transaction } from '../../interfaces/transaction.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { EnumService } from '../../services/enumService/enum.service';
import { catchError, throwError } from 'rxjs';
import { MatExpansionPanel, MatExpansionPanelHeader, MatExpansionPanelTitle, MatExpansionPanelDescription } from '@angular/material/expansion';
import { NgClass, CurrencyPipe, CommonModule } from '@angular/common';
import { I18nService } from '../../services/i18nService/i18n.service';

@Component({
  selector: 'app-transaction-panel',
  templateUrl: './transaction-panel.component.html',
  styleUrl: './transaction-panel.component.css',
  standalone: true,
  imports: [
    MatExpansionPanel,
    MatExpansionPanelHeader,
    MatExpansionPanelTitle,
    MatExpansionPanelDescription,
    NgClass,
    CurrencyPipe,
    CommonModule
  ]
})
export class TransactionPanelComponent {

  readonly transaction = input<Transaction>();

  private readonly enumService = inject(EnumService);
  private readonly i18nService = inject(I18nService);

  readonly transactionCategoryName = signal<string>("");

  readonly currencyCode = computed(() => {
    return this.i18nService.getCurrencyCode();
  });

  constructor() {
    effect(() => {
      const currentTransaction = this.transaction();

      if (!currentTransaction)
        return;

      const isIncomeTransaction = currentTransaction.type === 2;

      const categoryObservable = isIncomeTransaction
        ? this.enumService.getEnumIncomeCategories()
        : this.enumService.getEnumExpenseCategories();

      categoryObservable
        .pipe(catchError((error) => throwError(() => error)))
        .subscribe((response) => {
          const categoryKey = isIncomeTransaction
            ? currentTransaction.incomeCategory
            : currentTransaction.expenseCategory;

          this.transactionCategoryName.set(response[categoryKey]);
        })
    })
  }
}
