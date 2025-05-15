import { Component, inject, OnInit } from '@angular/core';
import { MonthlySummaryService } from '../../shared/services/monthySummaryService/monthly-summary.service';
import { catchError, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { AddTransactionDialogComponent } from '../../components/add-transaction-dialog/add-transaction-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-monthly-summaries-list',
  standalone: false,
  templateUrl: './monthly-summaries-list.component.html',
  styleUrl: './monthly-summaries-list.component.css'
})
export class MonthlySummariesListComponent implements OnInit {

  private monthlySummaryService = inject(MonthlySummaryService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);
  readonly addTransactionDialog = inject(MatDialog);


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

  onClickInAdd(): void {
    const dialogRef = this.addTransactionDialog.open(AddTransactionDialogComponent, {
      height: '80vh',
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadMonthlySummaries();
    });
  }

  onClickInCard(id: string): void {
    this.goToMonthlySummaryDetails(id);
  }

  goToMonthlySummaryDetails(id: string): void {
    this.router.navigate(['/monthly-summary', id]);
  }
}
