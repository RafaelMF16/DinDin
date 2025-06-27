import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { catchError, throwError } from 'rxjs';
import { TransactionService } from '../../services/transactionService/transaction.service';
import { ToastService } from '../../services/toastService/toast.service';
import { EnumService } from '../../services/enumService/enum.service';
import { MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-add-transaction-dialog',
  standalone: false,
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css'
})
export class AddTransactionDialogComponent implements OnInit {

  private transactionService = inject(TransactionService);
  private toastService = inject(ToastService);
  private enumService = inject(EnumService);

  readonly addTransactionDialog = inject(MatDialogRef<AddTransactionDialogComponent>);

  transactionForm!: FormGroup;

  categories?: string[];
  types?: string[];
  selectedType?: string;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForms();
    this.getEnums();
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

  getEnums(): void {
    this.getTypes();
  }

  getTypes(): void {
    this.enumService.getEnumType()
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível obter os tipos!");
          this.closeModal();
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.types = response;
      });
  }

  onSelectionChange(event: MatSelectChange): void {
    this.selectedType = event.value;

    const incomeType = "Income"
    if (this.selectedType == incomeType)
      this.getIncomeCategories();
    else
      this.getExpenseCategories();
  }

  getIncomeCategories(): void {
    this.enumService.getEnumIncomeCategories()
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível obter as categorias de renda!");
          this.closeModal();
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.categories = response;
      });
  }

  getExpenseCategories(): void {
    this.enumService.getEnumExpenseCategories()
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível obter as categorias de despesa!");
          this.closeModal();
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.categories = response;
      });
  }
}
