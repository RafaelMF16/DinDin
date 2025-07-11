import { Component, effect, inject, input, signal } from '@angular/core';
import { Transaction } from '../../interfaces/transaction.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { EnumService } from '../../services/enumService/enum.service';
import { catchError, throwError } from 'rxjs';
import { MatExpansionPanel, MatExpansionPanelHeader, MatExpansionPanelTitle, MatExpansionPanelDescription } from '@angular/material/expansion';
import { NgClass, CurrencyPipe, CommonModule } from '@angular/common';

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

  private readonly formatterService = inject(FormatterService);
  private readonly enumService = inject(EnumService);

  readonly formattedTransactionDate = signal<string>("");
  readonly transactionCategoryName = signal<string>("");

  constructor() {
    effect(() => {
      const currentTransaction = this.transaction();

      if (!currentTransaction) 
        return;

      // this.formattedTransactionDate.set(this.formatterService.formatteDate(currentTransaction.transactionDate));

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
