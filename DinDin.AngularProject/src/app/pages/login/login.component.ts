import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { slideContentTrigger } from '../../animations';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../../core/services/authService/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  animations: [slideContentTrigger],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  registerForm!: FormGroup;
  isLogin: boolean = true;

  private authService = inject(AuthService);
  private router = inject(Router);
  private snackBar = inject(MatSnackBar);

  constructor(
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initializeForms();
  }

  private initializeForms(): void {
    this.initializeLoginForms();
    this.initializeRegisterForms();
  }

  private initializeLoginForms(): void {
    this.loginForm = this.formBuilder.group({
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.email,
          Validators.pattern(/(.|\s)*\S(.|\s)*/),
        ]),
      ],
      password: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(50),
        ]),
      ],
    });
  }

  private initializeRegisterForms(): void {
    this.registerForm = this.formBuilder.group({
      name: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern(/(.|\s)*\S(.|\s)*/),
        ]),
      ],
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.email,
          Validators.pattern(/(.|\s)*\S(.|\s)*/),
        ]),
      ],
      password: [
        '',
        Validators.compose([
          Validators.required,
          Validators.minLength(8),
          Validators.maxLength(50),
        ]),
      ],
    });
  }

  onClickInSignUp(): void {
    this.isLogin = false;
    this.loginForm.reset();
    this.loginForm.markAsUntouched();
  }

  onClickInSignIn(): void {
    this.isLogin = true;
    this.registerForm.reset();
    this.registerForm.markAsUntouched();
  }

  onClickMakeRegister(): void {
    this.authService
      .createUser(this.registerForm.value)
      .pipe(
        catchError((error) => {
          this.openSnackBar("Erro");
          return throwError(() => new Error());
        })
      )
      .subscribe(() => {
        const successRegisterMessage = "Cadastro Realizado!";
        this.openSnackBar(successRegisterMessage);
        
        this.isLogin = true;
      });
  }

  onClickMakeLogin(): void {
    this.authService.login(this.loginForm.value)
      .pipe(
        catchError((error) => {
          this.openSnackBar("Erro");
          return throwError(() => new Error());
        })
      ).subscribe((response) => {
        const successLoginMessage = "Login Realizado!";
        this.openSnackBar(successLoginMessage);

        const keyName = "token";
        localStorage.setItem(keyName, response?.token);

        this.router.navigate(['/monthly-summaries']);
      });
  }

  openSnackBar(message: string) {
    this.snackBar.open(message, 'Fechar', {
      duration: 4000,
      horizontalPosition: 'center',
      verticalPosition: 'top'
    });
  }
}
