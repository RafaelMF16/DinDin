import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  
  private tokenKeyName: string = "token";

  getToken(): string | null{
    return sessionStorage.getItem(this.tokenKeyName);
  }

  setToken(keyName: string, token: any): void {
    sessionStorage.setItem(keyName, token)
  }

  clearToken(): void {
    sessionStorage.clear();
  }
}