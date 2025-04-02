import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { slideContentTrigger } from '../../animations';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../../core/services/authService/auth.service';

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

  constructor(private formBuilder: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void {
    this.initializeForms();
  }

  private initializeForms(): void {
    this.initializeLoginForms();
    this.initializeRegisterForms();
  }

  private initializeLoginForms(): void {
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

  private initializeRegisterForms(): void {
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

  aoClicarEmCadastro(): void {
    this.authService.createUser(this.registerForm.value)
      .pipe(catchError(error => {
        console.error(error);
        return throwError(() => new Error('Algo deu errado!'));
      })
      )
      .subscribe((response) => {
        console.log(response);
      });
  }
}