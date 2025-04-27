import { inject, Injectable } from '@angular/core';
import { HttpService } from '../../../core/services/httpService/http.service';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class MonthlySummaryService {

  private httpService = inject(HttpService);
  private jwtHelper = new JwtHelperService();

  getAllByUserId(): Observable<any> {
    let endpoint = "";
    const tokenKeyName = "token";
    const token = localStorage.getItem(tokenKeyName);
    if (!!token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      let userId = decodedToken?.nameid;
      endpoint = `MonthlySummary/get-all-with-user-id/${userId}`;
    }
    
    return this.httpService.get(endpoint);
  }
}
