import { Component, inject, OnInit } from '@angular/core';
import { MonthlySummaryService } from './service/monthly-summary.service';
import { catchError, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-monthly-summaries-list',
  standalone: false,
  templateUrl: './monthly-summaries-list.component.html',
  styleUrl: './monthly-summaries-list.component.css'
})
export class MonthlySummariesListComponent implements OnInit {

  private monthlySummaryService = inject(MonthlySummaryService);

  private snackBar = inject(MatSnackBar);

  monthlySummariesList: MonthlySummary[] = [];

  ngOnInit(): void {
    this.loadMonthlySummaries();
  }

  loadMonthlySummaries(): void {
    this.monthlySummaryService.getAllByUserId()
      .pipe(
        catchError((error) => {
          this.openSnackBar("Erro");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.monthlySummariesList = response;
      });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 4000,
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }
}
