import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { HttpService } from '../../core/services/httpService/http.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private httpService = inject(HttpService);

  addTransaction(transaction: FormGroup): Observable<any> {
    const endpoint = "MonthlySummary/add-transaction";
    return this.httpService.post<any>(endpoint, transaction.value);
  }
}
