import { Injectable, inject } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';
import { ErrorModalService } from '../../services/errorModalService/error-modal.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);
  private readonly errorModalService = inject(ErrorModalService);

  private isRefreshing = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  private isPublicAuthRoute(url: string): boolean {
    return ['login', 'register', 'verify-refresh-token', 'logout']
      .some(route => url.toLowerCase().includes(route));
  }

  intercept(request: HttpRequest<any>, httpHandler: HttpHandler): Observable<HttpEvent<any>> {
    const token = sessionStorage.getItem('token');
    const isAuthRoute = this.isPublicAuthRoute(request.url);

    let modifiedRequest = request;
    if (token && !isAuthRoute) {
      modifiedRequest = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${token}`)
      });
    }

    return httpHandler.handle(modifiedRequest).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401 && !isAuthRoute) {
          return this.verifyRefreshTokenAndTryAgain(modifiedRequest, httpHandler);
        }

        return throwError(() => error);
      })
    );
  }

  private verifyRefreshTokenAndTryAgain(request: HttpRequest<any>, httpHandler: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      const tokenKeyName = 'token';

      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.verifyRefreshToken().pipe(
        switchMap(response => {
          const newToken = response?.accessToken;
          sessionStorage.setItem(tokenKeyName, newToken);
          this.refreshTokenSubject.next(newToken);
          this.isRefreshing = false;

          const newRequest = request.clone({
            headers: request.headers.set('Authorization', `Bearer ${newToken}`)
          });
          return httpHandler.handle(newRequest);
        }),
        catchError(error => {
          this.isRefreshing = false;
          this.errorModalService.show(error?.error);

          if (sessionStorage.getItem(tokenKeyName)) {
            this.authService.logout().subscribe(() => {
              sessionStorage.clear();
              this.router.navigate(['/login']);
            });
          } else {
            this.router.navigate(['/login']);
          }

          return throwError(() => error);
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => !!token),
        take(1),
        switchMap(token => {
          const newRequest = request.clone({
            headers: request.headers.set('Authorization', `Bearer ${token}`)
          });
          return httpHandler.handle(newRequest);
        })
      );
    }
  }
}