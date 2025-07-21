import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { slideContentTrigger } from '../../animations';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../../core/services/authService/auth.service';
import { Router } from '@angular/router';
import { ToastService } from '../../services/toastService/toast.service';
import { MatDialog } from '@angular/material/dialog';
import { AllPageContainerComponent } from '../../components/all-page-container/all-page-container.component';
import { NgClass, NgIf } from '@angular/common';
import { FormsContainerComponent } from '../../components/forms-container/forms-container.component';
import { LabelInputComponent } from '../../components/label-input/label-input.component';
import { ErrorModalService } from '../../services/errorModalService/error-modal.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.css',
    standalone: true,
    animations: [slideContentTrigger],
    imports: [
        AllPageContainerComponent,
        NgClass,
        FormsContainerComponent,
        ReactiveFormsModule,
        LabelInputComponent,
        NgIf
    ],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  registerForm!: FormGroup;
  isLogin: boolean = true;

  private authService = inject(AuthService);
  private router = inject(Router);
  private toastService = inject(ToastService);
  private errorModalService = inject(ErrorModalService);

  readonly errorDialog = inject(MatDialog);

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
          this.errorModalService.show(error.error);
          return throwError(() => error);
        })
      )
      .subscribe(() => {
        const successRegisterMessage = "Cadastro Realizado!";
        this.toastService.openSnackBar(successRegisterMessage);
        this.registerForm.reset();
        this.registerForm.markAsUntouched();
        this.isLogin = true;
      });
  }

  onClickMakeLogin(): void {
    this.authService.login(this.loginForm.value)
      .pipe(
        catchError((error) => {
          this.errorModalService.show(error.error);
          return throwError(() => error);
        })
      ).subscribe((response) => {
        const successLoginMessage = "Login Realizado!";
        this.toastService.openSnackBar(successLoginMessage);

        const keyName = "token";
        sessionStorage.setItem(keyName, response?.accessToken);

        this.router.navigate(['/monthly-summaries']);
      });
  }
}
  