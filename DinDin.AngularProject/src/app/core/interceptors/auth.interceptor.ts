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

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = sessionStorage.getItem('token');
    const isAuthRoute = this.isPublicAuthRoute(req.url);

    let modifiedReq = req;
    if (token && !isAuthRoute) {
      modifiedReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
    }

    return next.handle(modifiedReq).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401 && !isAuthRoute) {
          return this.verifyRefreshTokenAndTryAgain(modifiedReq, next);
        }

        return throwError(() => error);
      })
    );
  }

  private verifyRefreshTokenAndTryAgain(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.verifyRefreshToken().pipe(
        switchMap(response => {
          const newToken = response?.accessToken;
          if (!newToken) throw new Error('Token vazio');

          sessionStorage.setItem('token', newToken);
          this.refreshTokenSubject.next(newToken);
          this.isRefreshing = false;

          const newRequest = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${newToken}`)
          });

          return next.handle(newRequest);
        }),
        catchError(error => {
          this.isRefreshing = false;
          this.errorModalService.show(error?.error ?? 'Sessão expirada.');

          if (sessionStorage.getItem('token')) {
            this.authService.logout().subscribe(() => {
              this.router.navigate(['/login']);
            });
          } else {
            this.router.navigate(['/login']);
          }

          return throwError(() => new Error('Sessão expirada'));
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => !!token),
        take(1),
        switchMap(token => {
          const newRequest = req.clone({
            headers: req.headers.set('Authorization', `Bearer ${token}`)
          });
          return next.handle(newRequest);
        })
      );
    }
  }
}

