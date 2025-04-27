import { Component, inject, OnInit } from '@angular/core';
import { MonthlySummaryService } from './service/monthly-summary.service';
import { catchError, throwError } from 'rxjs';
import { MonthlySummary } from '../../interfaces/monthly-summary.interface';

@Component({
  selector: 'app-monthly-summaries-list',
  standalone: false,
  templateUrl: './monthly-summaries-list.component.html',
  styleUrl: './monthly-summaries-list.component.css'
})
export class MonthlySummariesListComponent implements OnInit {

  private monthlySummaryService = inject(MonthlySummaryService);

  monthlySummariesList: MonthlySummary[] = [];
  isLoading: boolean = false;

  ngOnInit(): void {
    this.isLoading = true;
    this.loadMonthlySummaries();
  }

  loadMonthlySummaries(): void {
    this.monthlySummaryService.getAllByUserId()
      .pipe(
        catchError((error) => {
          console.log(error);
          return throwError(() => new Error('Algo deu errado!'));
        })
      ).subscribe((response) => {
        this.monthlySummariesList = response;
        this.isLoading = false;
      });
  }
}
