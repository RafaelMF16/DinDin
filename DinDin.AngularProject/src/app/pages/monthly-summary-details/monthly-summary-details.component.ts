import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, Subscription, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MonthlySummaryService } from '../../services/monthlySummaryService/monthly-summary.service';
import { ToastService } from '../../services/toastService/toast.service';

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

  private subscription?: Subscription;
  monthlySummary?: MonthlySummary;
  private id: string = '';

  ngOnInit(): void {
    this.getIdByRoute();
    this.loadMonthlySummary(this.id);
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  loadMonthlySummary(id: string): void {
    this.subscription = this.monthlySummaryService.getById(id)
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível carregar o resumo mensal!");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.monthlySummary = response;
      });
  }

  getIdByRoute(): void {
    const idParameter = "id";
    this.id = this.route.snapshot.paramMap.get(idParameter)!;
  }

  onClickInNavBack(): void {
    this.router.navigate(["/monthly-summaries"]);
  }
}
