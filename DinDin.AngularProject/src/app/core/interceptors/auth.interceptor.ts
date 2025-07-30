import { Injectable, inject } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, filter, switchMap, take, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../services/authService/auth.service';
import { ErrorModalService } from '../../services/errorModalService/error-modal.service';
import { TokenService } from '../../services/tokenService/token.service';
import { LoadingService } from '../../services/loadingService/loading.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private readonly router = inject(Router);
  private readonly authService = inject(AuthService);
  private readonly errorModalService = inject(ErrorModalService);
  private readonly tokenService = inject(TokenService);
  private readonly loadingService = inject(LoadingService);

  private isRefreshing = false;
  private refreshTokenSubject = new BehaviorSubject<string | null>(null);

  private isPublicAuthRoute(url: string): boolean {
    return [
      '/api/auth/login',
      '/api/auth/register',
      '/api/auth/verify-refresh-token',
      '/api/auth/logout'
    ].some(route => url.toLowerCase().includes(route));
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.tokenService.getToken();
    const isAuthRoute = this.isPublicAuthRoute(request.url);

    if (token && !isAuthRoute) {
      request = this.addTokenToRequest(request, token);
    }

    return next.handle(request).pipe(
      catchError(error => {
        if (
          error instanceof HttpErrorResponse &&
          error.status === 401 &&
          !isAuthRoute
        ) {
          return this.handle401Error(request, next);
        }

        return throwError(() => error);
      })
    );
  }

  private addTokenToRequest(request: HttpRequest<any>, token: string): HttpRequest<any> {
    return request.clone({
      headers: request.headers.set('Authorization', `Bearer ${token}`)
    });
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      this.loadingService.show();

      return this.authService.verifyRefreshToken().pipe(
        switchMap(response => {
          const newToken = response?.accessToken;
          this.tokenService.setToken('token', newToken);
          this.refreshTokenSubject.next(newToken);
          this.isRefreshing = false;

          return next.handle(this.addTokenToRequest(request, newToken));
        }),
        catchError(error => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(null);

          if (!this.errorModalService.isModalShown()) {
            this.errorModalService.show(error?.error);
          }
          this.tokenService.clearToken();
          this.loadingService.show();
          this.authService.logout().subscribe({
            complete: () => {
              this.loadingService.hide();
              this.router.navigate(['/login']);
            },
            error: () => {
              this.loadingService.hide();
              this.router.navigate(['/login']);
            }
          });

          return throwError(() => error);
        }),
        finalize(() => {
          this.loadingService.hide();
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => !!token),
        take(1),
        switchMap(token => next.handle(this.addTokenToRequest(request, token!)))
      );
    }
  }
}