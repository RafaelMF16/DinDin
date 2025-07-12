import { Inject, Injectable, LOCALE_ID } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class I18nService {

  private currencyMap: { [key: string]: string } = {
    'pt': 'BRL',
    'es': 'EUR',
    'en-US': 'USD'
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
}
