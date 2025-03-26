import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private url: string = "https://localhost:7226/api/Auth/Register";

  constructor(private http: HttpClient) { }

  makeRegister(registerForm: string) {
    return this.http.post(this.url, registerForm);
  }
}
