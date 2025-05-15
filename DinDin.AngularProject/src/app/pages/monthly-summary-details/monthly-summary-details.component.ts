import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MonthlySummaryService } from '../../shared/services/monthySummaryService/monthly-summary.service';
import { catchError, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Transaction } from '../../interfaces/transaction.interface';

@Component({
  selector: 'app-monthly-summary-details',
  standalone: false,
  templateUrl: './monthly-summary-details.component.html',
  styleUrl: './monthly-summary-details.component.css'
})
export class MonthlySummaryDetailsComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private monthlySummaryService = inject(MonthlySummaryService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  monthlySummary: MonthlySummary = {} as MonthlySummary;

  ngOnInit(): void {
    const id = this.getIdByRoute();
    this.loadMonthlySummary(id);
  }

  loadMonthlySummary(id: string): void {
    this.monthlySummaryService.getById(id)
      .pipe(
        catchError(() => {
          this.openSnackBar("Erro");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.monthlySummary = response;
      });
  }

  getIdByRoute(): string {
    const idParameter = "id";
    const id = this.route.snapshot.paramMap.get(idParameter);

    if (!!id)
      return id;

    const emptyString = '';
    return emptyString;
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
