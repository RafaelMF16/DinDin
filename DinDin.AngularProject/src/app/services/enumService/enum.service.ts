import { inject, Injectable } from '@angular/core';
import { HttpService } from '../../core/services/httpService/http.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnumService {

  private httpService = inject(HttpService);

  getEnumType(): Observable<any> {
    const endpoint = "Enum/Type"
    return this.httpService.get(endpoint);
  }

  getEnumIncomeCategories(): Observable<any> {
    const endpoint = "Enum/IncomeCategories"
    return this.httpService.get(endpoint);
  }

  getEnumExpenseCategories(): Observable<any> {
    const endpoint = "Enum/ExpenseCategories"
    return this.httpService.get(endpoint);
  }
}
