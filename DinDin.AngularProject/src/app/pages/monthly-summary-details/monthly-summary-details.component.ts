import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MonthlySummaryService } from '../../shared/services/monthySummaryService/monthly-summary.service';
import { catchError, Subscription, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-monthly-summary-details',
  standalone: false,
  templateUrl: './monthly-summary-details.component.html',
  styleUrl: './monthly-summary-details.component.css'
})
export class MonthlySummaryDetailsComponent implements OnInit, OnDestroy {
  private route = inject(ActivatedRoute);
  private monthlySummaryService = inject(MonthlySummaryService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  private subscription: Subscription = new Subscription();
  monthlySummary?: MonthlySummary;
  private id: string = '';

  ngOnInit(): void {
    this.getIdByRoute();
    this.loadMonthlySummary(this.id);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  loadMonthlySummary(id: string): void {
    this.subscription = this.monthlySummaryService.getById(id)
      .pipe(
        catchError(() => {
          this.openSnackBar("Erro");
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

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 4000,
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }

  onClickInNavBack(): void {
    this.router.navigate(["/monthly-summaries"]);
  }
}
