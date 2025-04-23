import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../httpService/http.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private httpService = inject(HttpService);

  createUser(user: any): Observable<any> {
    const endpoint = "Auth/Register";
    return this.httpService.post<any>(endpoint, user);
  }

  login(user: any): Observable<any> {
    const endpoint = "Auth/Login";
    return this.httpService.post<any>(endpoint, user);
  }
}
