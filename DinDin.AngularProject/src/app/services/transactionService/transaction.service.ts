import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpService } from '../../core/services/httpService/http.service';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  private httpService = inject(HttpService);
  private jwtHelper = new JwtHelperService();

  addTransaction(transaction: FormGroup): Observable<any> {
    const tokenKeyName = "token";
    const token = localStorage.getItem(tokenKeyName);
    if (!!token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      let userId = decodedToken?.nameid;
      
      const userIdControlName = "userId"
      transaction.get(userIdControlName)?.setValue(userId);
    }
    const endpoint = "MonthlySummary/add-transaction";
    return this.httpService.post<any>(endpoint, transaction.value);
  }
}
