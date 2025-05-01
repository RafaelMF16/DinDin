import { Component, inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatCalendarCellClassFunction } from '@angular/material/datepicker';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-transaction-dialog',
  standalone: false,
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css'
})
export class AddTransactionDialogComponent {
  readonly addTransactionDialog = inject(MatDialogRef<AddTransactionDialogComponent>);

  dataEscolhida = new FormControl(new Date());

  onClickCancel(): void  {
    this.addTransactionDialog.close();
  }

  categories: string[] =  [
    "Alimentação",
    "Entreterimento",
    "Transporte",
    "Saúde",
    "Educação",
    "Investimentos",
    "Outros"
  ]
}
