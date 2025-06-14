import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule, provideHttpClient } from '@angular/common/http';
import { AppConfig } from './config/app-config';
import { environment } from '../environments/environments';
import { AllPageContainerComponent } from './components/all-page-container/all-page-container.component';
import { FormsContainerComponent } from './components/forms-container/forms-container.component';
import { LabelInputComponent } from './components/label-input/label-input.component';
import { MonthlySummariesListComponent } from './pages/monthly-summaries-list/monthly-summaries-list.component';
import { MonthlySummariesContainerComponent } from './components/monthly-summaries-container/monthly-summaries-container.component';
import { MonthlySummaryCardComponent } from './components/monthly-summary-card/monthly-summary-card.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { HeaderComponent } from './components/header/header.component';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDialogModule } from '@angular/material/dialog';
import { AddTransactionDialogComponent } from './components/add-transaction-dialog/add-transaction-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import {MatRadioModule} from '@angular/material/radio';
import { MonthlySummaryDetailsComponent } from './pages/monthly-summary-details/monthly-summary-details.component';
import {MatListModule} from '@angular/material/list';
import {MatExpansionModule} from '@angular/material/expansion';
import { DetailsContainerComponent } from './components/details-container/details-container.component';
import { MonthlySummaryDetailsCardComponent } from './components/monthly-summary-details-card/monthly-summary-details-card.component';
import { MonthlySummaryDetailsToolbarComponent } from './components/monthly-summary-details-toolbar/monthly-summary-details-toolbar.component';
import { TransactionPanelComponent } from './components/transaction-panel/transaction-panel.component';
import { DatePipe } from '@angular/common';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { ErrorDialogComponent } from './components/error-dialog/error-dialog.component';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AllPageContainerComponent,
    FormsContainerComponent,
    LabelInputComponent,
    MonthlySummariesListComponent,
    MonthlySummariesContainerComponent,
    MonthlySummaryCardComponent,
    HeaderComponent,
    AddTransactionDialogComponent,
    MonthlySummaryDetailsComponent,
    DetailsContainerComponent,
    MonthlySummaryDetailsCardComponent,
    MonthlySummaryDetailsToolbarComponent,
    TransactionPanelComponent,
    ErrorDialogComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
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
  ],
  providers: [
    provideRouter(routes),
    { 
      provide: MAT_DATE_LOCALE, 
      useValue: 'pt-BR'
    },
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    AppConfig.apiBaseUrl = environment.apiBaseUrl
  }
}
