import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { catchError, throwError } from 'rxjs';
import { TransactionService } from '../../services/transactionService/transaction.service';
import { ToastService } from '../../services/toastService/toast.service';

@Component({
  selector: 'app-add-transaction-dialog',
  standalone: false,
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css'
})
export class AddTransactionDialogComponent implements OnInit {

  private transactionService = inject(TransactionService);
  private toastService = inject(ToastService);
  
  readonly addTransactionDialog = inject(MatDialogRef<AddTransactionDialogComponent>);

  transactionForm!: FormGroup;

  categories: string[] = [
    "Alimentação",
    "Entreterimento",
    "Transporte",
    "Saúde",
    "Educação",
    "Investimentos",
    "Outros"
  ]

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
      ],
      userId: [
        ''
      ]
    })
  }

  onClickCancel(): void {
    this.closeModal();
  }

  closeModal(): void {
    this.addTransactionDialog.close();
  }

  onClickInSave(): void {
    this.addTransaction();
  }

  addTransaction(): void {
    this.transactionService.addTransaction(this.transactionForm)
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível cadastrar transação!");
          return throwError(() => new Error());
        })
      ).subscribe(() => {
        const successMessage = "Transação cadastrada com sucesso!"
        this.toastService.openSnackBar(successMessage);
        this.closeModal();
      });
  }
}
