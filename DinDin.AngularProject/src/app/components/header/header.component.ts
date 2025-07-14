import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatMenuTrigger, MatMenu, MatMenuItem } from '@angular/material/menu';

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

  private router = inject(Router);

  onClickInLogout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  onClickInPt(): void {

  }

  onClickInUs(): void {

  }

  onClickInEs(): void {

  }
}
