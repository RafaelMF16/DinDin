import { Inject, Injectable, LOCALE_ID } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class I18nService {

  private currencyMap: { [key: string]: string } = {
    'pt': 'BRL',
    'es': 'EUR',
    'en': 'USD'
  };

  constructor(@Inject(LOCALE_ID) private locale: string) { }

  setLocale(locale: string) {
    this.locale = locale;
  }

  getLocale(): string {
    return this.locale;
  }

  getCurrencyCode(): string {
    return this.currencyMap[this.locale] || 'BRL';
  }

  changeLanguage(locale: string) {
    const currentPath = window.location.pathname;
    const cleanPath = currentPath.replace(/^\/(pt|en|es)(\/|$)/, '/');

    let targetBaseHref = '/';
    if (locale !== 'pt')
      targetBaseHref = '/' + locale + '/';
    else
      targetBaseHref = '/pt/';

    const normalizedPath = cleanPath.startsWith('/')
      ? cleanPath.slice(1)
      : cleanPath;

    window.location.href = `${targetBaseHref}${normalizedPath}`;
  }
}
