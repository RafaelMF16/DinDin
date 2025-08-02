import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoadingComponent } from "./components/loading/loading.component";
import { NgIf } from '@angular/common';
import { LoadingService } from './services/loadingService/loading.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    standalone: true,
    imports: [RouterOutlet, LoadingComponent, NgIf]
})
export class AppComponent {
  title = 'DinDin';

  constructor(public loadingService: LoadingService) {}
}
