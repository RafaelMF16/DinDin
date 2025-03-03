import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { slideContentTrigger } from '../../animations';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  animations: [slideContentTrigger]
})
export class LoginComponent implements OnInit {
  
  loginForm!: FormGroup;
  registerForm!: FormGroup;
  isLogin: boolean = true;

  constructor(private formBuilder: FormBuilder) { }
  
  ngOnInit(): void {
    this.initializeForms();
  }

  initializeForms(): void {
    this.initializeLoginForms();
    this.initializeRegisterForms();
  }

  initializeLoginForms(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.compose([
        Validators.required,
        Validators.email,
        Validators.pattern(/(.|\s)*\S(.|\s)*/)
      ])],
      password: ['', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(50)
      ])]
    });
  }

  initializeRegisterForms(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', Validators.compose([
        Validators.required,
        Validators.pattern(/(.|\s)*\S(.|\s)*/)
      ])],
      email: ['', Validators.compose([
        Validators.required, 
        Validators.email,
        Validators.pattern(/(.|\s)*\S(.|\s)*/)
      ])],
      password: ['', Validators.compose([
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(50)
      ])]
    });
  }

  onClickInSignUp(): void {
    this.isLogin = false;
  }

  onClickInSignIn(): void {
    this.isLogin = true;
  }
}