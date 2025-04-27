import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
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
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatDialogModule } from '@angular/material/dialog';
import { ErrorModalComponent } from './components/error-modal/error-modal.component';

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
    ErrorModalComponent
  ],
  imports: [
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
    MatDialogModule
  ],
  providers: [
    provideRouter(routes),
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {
    AppConfig.apiBaseUrl = environment.apiBaseUrl
  }
}
