/// <reference types="@angular/localize" />

import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { HTTP_INTERCEPTORS, withInterceptorsFromDi, provideHttpClient } from '@angular/common/http';
import { AuthInterceptor } from './app/core/interceptors/auth.interceptor';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { provideAnimations } from '@angular/platform-browser/animations';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatRadioModule } from '@angular/material/radio';
import { MatListModule } from '@angular/material/list';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatMenuModule } from '@angular/material/menu';
import { AppComponent } from './app/app.component';
import { importProvidersFrom } from '@angular/core';

bootstrapApplication(AppComponent, {
    providers: [
        importProvidersFrom(
            BrowserModule,
            ReactiveFormsModule,
            MatDividerModule,
            MatCardModule,
            MatButtonModule,
            MatIconModule,
            MatTooltipModule,
            MatToolbarModule,
            MatDialogModule,
            MatFormFieldModule,
            MatInputModule,
            MatSelectModule,
            MatCheckboxModule,
            MatDatepickerModule,
            MatNativeDateModule,
            MatRadioModule,
            MatListModule,
            MatExpansionModule,
            MatMenuModule
        ),
        provideRouter(routes),
        {
            provide: MAT_DATE_LOCALE,
            useValue: 'pt-BR'
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideAnimations()
    ]
})
  .catch(err => console.error(err));
