import { Component, inject, Input, OnInit, signal } from '@angular/core';
import { Transaction } from '../../interfaces/transaction.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';
import { EnumService } from '../../services/enumService/enum.service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-transaction-panel',
  standalone: false,
  templateUrl: './transaction-panel.component.html',
  styleUrl: './transaction-panel.component.css'
})
export class TransactionPanelComponent implements OnInit {

  private formatterService = inject(FormatterService);
  private enumService = inject(EnumService);

  category = signal("");
  formattedDate = signal("");

  @Input() transaction!: Transaction;

  ngOnInit(): void {
    this.formattedDate.set(this.formatterService.formatteDate(this.transaction.transactionDate));
    this.carregarCategory();
  }

  carregarCategory(): void {
    this.enumService.getEnumIncomeCategories().pipe(
      catchError(() => {
        return throwError(() => new Error())
      })).subscribe((response) => {
        this.category.set(response[this.transaction.incomeCategory]);
      });
  }
}
