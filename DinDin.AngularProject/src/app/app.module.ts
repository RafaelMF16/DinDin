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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AllPageContainerComponent,
    FormsContainerComponent,
    LabelInputComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
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
