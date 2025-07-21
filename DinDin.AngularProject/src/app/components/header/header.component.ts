import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatMenuTrigger, MatMenu, MatMenuItem } from '@angular/material/menu';
import { I18nService } from '../../services/i18nService/i18n.service';

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
export class HeaderComponent {

  private readonly router = inject(Router);
  private readonly i18nService = inject(I18nService);

  onClickInLogout(): void {
    sessionStorage.clear();
    this.router.navigate(['/login']);
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
}
