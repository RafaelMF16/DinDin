import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  private router = inject(Router);
  
  onClickInLogout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }
}
