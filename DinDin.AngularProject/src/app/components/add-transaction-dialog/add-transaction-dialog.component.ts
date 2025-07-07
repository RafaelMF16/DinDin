import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MatDialogTitle, MatDialogContent, MatDialogActions } from '@angular/material/dialog';
import { catchError, throwError } from 'rxjs';
import { TransactionService } from '../../services/transactionService/transaction.service';
import { ToastService } from '../../services/toastService/toast.service';
import { EnumService } from '../../services/enumService/enum.service';
import { MatSelectChange, MatSelect, MatOption } from '@angular/material/select';
import { MatDivider } from '@angular/material/divider';
import { CdkScrollable } from '@angular/cdk/scrolling';
import { FormsContainerComponent } from '../forms-container/forms-container.component';
import { MatFormField, MatLabel, MatPrefix, MatError, MatSuffix } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDatepickerInput, MatDatepickerToggle, MatDatepicker } from '@angular/material/datepicker';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-add-transaction-dialog',
  templateUrl: './add-transaction-dialog.component.html',
  styleUrl: './add-transaction-dialog.component.css',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDivider,
    CdkScrollable,
    MatDialogContent,
    FormsContainerComponent,
    ReactiveFormsModule,
    MatFormField,
    MatLabel,
    MatInput,
    MatPrefix,
    MatError,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatSuffix,
    MatDatepicker,
    MatSelect,
    MatOption,
    MatDialogActions,
    NgClass
  ]
})
export class AddTransactionDialogComponent implements OnInit {

  private transactionService = inject(TransactionService);
  private toastService = inject(ToastService);
  private enumService = inject(EnumService);

  readonly addTransactionDialog = inject(MatDialogRef<AddTransactionDialogComponent>);

  transactionForm!: FormGroup;

  categories?: { key: number, value: string }[];
  types?: { key: number, value: string }[];
  selectedType?: number;
  isIncome?: boolean;

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
      ],
      incomeCategory: [],
      expenseCategory: []
    })
  }

  onClickCancel(): void {
    this.closeModal();
  }

  closeModal(): void {
    this.addTransactionDialog.close();
  }

  onClickInSave(): void {
    let formValue = this.transactionForm.value;
    const transaction: any = {
      amout: formValue.amont,
      transactionDate: formValue.transactionDate,
      type: formValue.type,
      description: formValue.description
    };

    if (this.isIncome) {
      formValue.incomeCategory = formValue.category;
      transaction.incomeCategories = formValue.incomeCategory;
    }
    else {
      formValue.expenseCategory = formValue.category
      transaction.expenseCategories = formValue.expenseCategory;
    }

    this.addTransaction(formValue);
  }

  addTransaction(transaction: any): void {
    this.transactionService.addTransaction(transaction)
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
          this.toastService.openSnackBar("Não foi possível obter os tipos de transação!");
          this.closeModal();
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.types = Object.entries(response).map(([key, value]) => ({
          key: Number(key),
          value: String(value)
        }));
      });
  }

  onSelectionChange(event: MatSelectChange): void {
    this.selectedType = event.value;

    const incomeType = 2
    if (this.selectedType == incomeType) {
      this.isIncome = true;
      this.getIncomeCategories();
    }
    else {
      this.isIncome = false;
      this.getExpenseCategories();
    }
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
        this.categories = Object.entries(response).map(([key, value]) => ({
          key: Number(key),
          value: String(value)
        }));
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
        this.categories = Object.entries(response).map(([key, value]) => ({
          key: Number(key),
          value: String(value)
        }));
      });
  }
}
