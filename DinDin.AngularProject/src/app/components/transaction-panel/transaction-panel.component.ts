import { Component, inject, Input } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-transaction-panel',
  standalone: false,
  templateUrl: './transaction-panel.component.html',
  styleUrl: './transaction-panel.component.css'
})
export class TransactionPanelComponent {

  private datePipe = inject(DatePipe);

  @Input() type!: string;
  @Input() category!: string;
  @Input() amont!: number;
  @Input() description!: string;

  @Input()
  set transactionDate(value: string) {
    const datePattern = 'dd/MMM/yyyy'
    this._transactionDate = this.datePipe.transform(value, datePattern)!;

  }

  _transactionDate!: string;
}
