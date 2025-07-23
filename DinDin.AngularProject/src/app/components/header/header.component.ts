import { Component, inject, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { MatMenuTrigger, MatMenu, MatMenuItem } from '@angular/material/menu';
import { I18nService } from '../../services/i18nService/i18n.service';
import { AuthService } from '../../core/services/authService/auth.service';
import { catchError, EMPTY, Observable, Subscription, switchMap, throwError } from 'rxjs';
import { ErrorModalService } from '../../services/errorModalService/error-modal.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  standalone: true,
  imports: [
    MatMenuTrigger,
    MatMenu,
    MatMenuItem,
  ]
})
export class HeaderComponent implements OnDestroy{

  private readonly router = inject(Router);
  private readonly i18nService = inject(I18nService);
  private readonly authService = inject(AuthService);
  private readonly errorModalService = inject(ErrorModalService);
  private mySubscription?: Subscription;

  makeLogout(): void {
    this.mySubscription = this.authService.logout().pipe(
      catchError(error => this.renewAccessToken(error))
    ).subscribe(() => {
      sessionStorage.clear();
      this.router.navigate(['/login']);
    });
  }

  renewAccessToken(error: any): Observable<any> {
    const unauthorizedCode = 401;
    if (error.status === unauthorizedCode) {
      return this.authService.verifyRefreshToken().pipe(
        switchMap((response) => {
          const keyName = "token";
          sessionStorage.setItem(keyName, response?.accessToken);
          return this.authService.logout();
        }),
        catchError((refreshError) => {
          this.errorModalService.show(refreshError.error);
          return EMPTY;
        })
      )
    }

    this.errorModalService.show(error.error);
    return EMPTY;
  }

  onClickInLogout(): void {
    this.makeLogout();
  }

  onClickInPt(): void {
    const ptLocale: string = "pt"
    this.i18nService.changeLanguage(ptLocale);
  }

  onClickInUs(): void {
    const usLocale: string = "en"
    this.i18nService.changeLanguage(usLocale);
  }

  onClickInEs(): void {
    const esLocale: string = "es"
    this.i18nService.changeLanguage(esLocale);
  }

  ngOnDestroy(): void {
    this.mySubscription?.unsubscribe();
  }
}
