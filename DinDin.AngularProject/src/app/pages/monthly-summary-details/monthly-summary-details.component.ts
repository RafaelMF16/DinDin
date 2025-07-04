import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MonthlySummaryService } from '../../services/monthlySummaryService/monthly-summary.service';
import { ToastService } from '../../services/toastService/toast.service';
import { TransactionService } from '../../services/transactionService/transaction.service';
import { Transaction } from '../../interfaces/transaction.interface';

@Component({
  selector: 'app-monthly-summary-details',
  standalone: false,
  templateUrl: './monthly-summary-details.component.html',
  styleUrl: './monthly-summary-details.component.css'
})
export class MonthlySummaryDetailsComponent implements OnInit, OnDestroy {
  private route = inject(ActivatedRoute);
  private monthlySummaryService = inject(MonthlySummaryService);
  private router = inject(Router);
  private toastService = inject(ToastService);
  private transactionService = inject(TransactionService);
  private subscription?: Subscription;
  private id?: number;

  monthlySummary?: MonthlySummary;
  transactions?: Transaction[];

  ngOnInit(): void {
    this.getIdByRoute();
    this.loadMonthlySummary();
    this.loadTransactions();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  loadMonthlySummary(): void {
    this.subscription = this.monthlySummaryService.getById(this.id)
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível carregar o resumo mensal!");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.monthlySummary = response;
      });
  }

  loadTransactions(): void {
    this.subscription = this.transactionService.getAllByMonthlySummaryId(this.id)
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível carregar as transações!");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.transactions = response;
      });
  }

  getIdByRoute(): void {
    const idParameter = "id";
    this.id = Number(this.route.snapshot.paramMap.get(idParameter))!;
  }

  onClickInNavBack(): void {
    this.router.navigate(["/monthly-summaries"]);
  }
}
