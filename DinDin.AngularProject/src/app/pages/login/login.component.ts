import { state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { slideContentTrigger } from '../../animations';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  animations: [slideContentTrigger]
})
export class LoginComponent {
  isLogin: boolean = true;

  onClickInSignUp(): void{
    this.isLogin = false;
  }

  onClickInSignIn(): void {
    this.isLogin = true;
  }
}