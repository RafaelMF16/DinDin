import { Component, inject, OnInit } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';
import { MatDialog } from '@angular/material/dialog';
import { AddTransactionDialogComponent } from '../../components/add-transaction-dialog/add-transaction-dialog.component';
import { Router } from '@angular/router';
import { MonthlySummaryService } from '../../services/monthlySummaryService/monthly-summary.service';
import { ToastService } from '../../services/toastService/toast.service';

@Component({
  selector: 'app-monthly-summaries-list',
  standalone: false,
  templateUrl: './monthly-summaries-list.component.html',
  styleUrl: './monthly-summaries-list.component.css'
})
export class MonthlySummariesListComponent implements OnInit {

  private monthlySummaryService = inject(MonthlySummaryService);
  private router = inject(Router);
  private toastService = inject(ToastService);

  readonly addTransactionDialog = inject(MatDialog);


  monthlySummariesList?: MonthlySummary[];

  ngOnInit(): void {
    this.loadMonthlySummaries();
  }

  loadMonthlySummaries(): void {
    this.monthlySummaryService.getAllByUserId()
      .pipe(
        catchError(() => {
          this.toastService.openSnackBar("Não foi possível carregar os resumos mensais!");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        this.monthlySummariesList = response;
      });
  }

  onClickInAdd(): void {
    const dialogRef = this.addTransactionDialog.open(AddTransactionDialogComponent, {
      height: '75vh',
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
