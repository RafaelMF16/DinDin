import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpService } from '../../core/services/httpService/http.service';
import { TokenService } from '../tokenService/token.service';


@Injectable({
  providedIn: 'root'
})
export class MonthlySummaryService {

  private httpService = inject(HttpService);
  private jwtHelper = new JwtHelperService();
  private readonly tokenService = inject(TokenService);

  getAllByUserId(): Observable<any> {
    let endpoint = "";
    const token = this.tokenService.getToken();
    const decodedToken = this.jwtHelper.decodeToken(token!);
    let userId = decodedToken?.nameid;
    endpoint = `MonthlySummary/get-all-with-user-id/${userId}`;

    return this.httpService.get(endpoint);
  }

  getById(id?: number): Observable<any> {
    let endpoint = `MonthlySummary/${id}`;
    return this.httpService.get(endpoint);
  }
}
