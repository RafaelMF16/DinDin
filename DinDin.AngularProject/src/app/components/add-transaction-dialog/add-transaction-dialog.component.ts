import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-transaction-dialog',
  standalone: false,
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css'
})
export class AddTransactionDialogComponent implements OnInit {
  readonly addTransactionDialog = inject(MatDialogRef<AddTransactionDialogComponent>);

  transactionForm!: FormGroup;
  isIncome: boolean = true;
  isExpense: boolean = false;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForms();
  }

  private initializeForms(): void {
    this.initializeTransactionForms();
  }

  private initializeTransactionForms(): void {
    this.transactionForm = this.formBuilder.group({
      amont: [
        0,
        Validators.compose([
          Validators.required,
        ])
      ],
      category: [
        '',
        Validators.compose([
          Validators.required,
        ])
      ],
      transactionDate: [
        '',
        Validators.compose([
          Validators.required,
        ])
      ],
      type: [
        '',
        Validators.compose([
          Validators.required,
        ])
      ],
      description: [
        '',
        Validators.compose([
          Validators.required,
        ])
      ]
    })
  }

  onClickCancel(): void {
    this.addTransactionDialog.close();
  }

  teste(): void {
    console.log(this.transactionForm.value)
  }

  categories: string[] = [
    "Alimentação",
    "Entreterimento",
    "Transporte",
    "Saúde",
    "Educação",
    "Investimentos",
    "Outros"
  ]
}
