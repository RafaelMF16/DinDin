import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../../core/services/httpService/http.service';

@Injectable({
  providedIn: 'root'
})
export class MonthlySummaryService {

  private httpService = inject(HttpService);

  getAllByUserId(): Observable<any> {
    const endpoint = "MonthlySummary/get-all-with-user-id";
    return this.httpService.get(endpoint);
  }

  getById(id?: number): Observable<any> {
    let endpoint = `MonthlySummary/${id}`;
    return this.httpService.get(endpoint);  
  }
}
