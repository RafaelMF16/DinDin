import { Component, inject, Input, OnInit } from '@angular/core';
import { Transaction } from '../../interfaces/transaction.interface';
import { FormatterService } from '../../services/formatterService/formatter.service';

@Component({
  selector: 'app-transaction-panel',
  standalone: false,
  templateUrl: './transaction-panel.component.html',
  styleUrl: './transaction-panel.component.css'
})
export class TransactionPanelComponent implements OnInit {

  private formatterService = inject(FormatterService);

  @Input() transaction!: Transaction;

  ngOnInit(): void {
    this.transaction.transactionDate = this.formatterService.formatteDate(this.transaction.transactionDate);
  }
}
