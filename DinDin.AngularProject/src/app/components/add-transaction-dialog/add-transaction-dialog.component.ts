import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { TransactionService } from './services/transaction.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-add-transaction-dialog',
  standalone: false,
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css'
})
export class AddTransactionDialogComponent implements OnInit {
  readonly addTransactionDialog = inject(MatDialogRef<AddTransactionDialogComponent>);
  private transactionService = inject(TransactionService);
  private snackBar = inject(MatSnackBar);

  transactionForm!: FormGroup;

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
          Validators.required
        ])
      ],
      category: [
        '',
        Validators.compose([
          Validators.required
        ])
      ],
      transactionDate: [
        '',
        Validators.compose([
          Validators.required
        ])
      ],
      type: [
        '',
        Validators.compose([
          Validators.required
        ])
      ],
      description: [
        '',
        Validators.compose([
          Validators.required
        ])
      ]
    })
  }

  onClickCancel(): void {
    this.addTransactionDialog.close();
  }

  onClickInSave(): void {
    this.addTransaction();
  }

  addTransaction(): void {
    this.transactionService.addTransaction(this.transactionForm.value)
      .pipe(
        catchError(() => {
          this.openSnackBar("Erro");
          return throwError(() => new Error());
        })
      ).subscribe(() => {
        const successMessage = "Transação cadastrada com sucesso!"
        this.openSnackBar(successMessage);
      });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 4000,
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
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
