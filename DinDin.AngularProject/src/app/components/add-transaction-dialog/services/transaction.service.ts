import { inject, Injectable } from '@angular/core';
import { HttpService } from '../../../core/services/httpService/http.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private httpService = inject(HttpService);

  addTransaction(transaction: any): Observable<any> {
    const endpoint = "Transaction";
    return this.httpService.post<any>(endpoint, transaction);
  }
}
