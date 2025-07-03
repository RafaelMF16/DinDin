import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../../core/services/httpService/http.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private httpService = inject(HttpService);

  addTransaction(transaction: any): Observable<any> {
    const endpoint = "Transaction";
    return this.httpService.post<any>(endpoint, transaction);
  }

  getAllByMonthlySummaryId(monthlySummaryId?: number): Observable<any> {
    const endpoint = `Transaction/get-all-by-monthly-summary-id/${monthlySummaryId}`;
    return this.httpService.get(endpoint);
  }
}
