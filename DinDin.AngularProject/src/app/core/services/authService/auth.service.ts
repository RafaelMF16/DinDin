import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../httpService/http.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpService: HttpService) { }

  createUser(user: any): Observable<any> {
    const endpoint = "Auth/Register";
    return this.httpService.post<any>(endpoint, user);
  }
}
