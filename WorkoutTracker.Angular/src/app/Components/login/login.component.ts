import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthResponse, ExternalAuthentication, UserForAuthentication } from '../../Models/user.model';
import { AuthService } from '../../Services/auth.service';
import { LocalStorageService } from '../../Services/local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  errorMessage: string = '';
  showError: boolean = false;

  constructor(
    private router: Router,
    private http: HttpClient,
    private authService: AuthService,
    private localStorageService: LocalStorageService)
  { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    })

    this.authService.extAuthChanged.subscribe(user => {
      const externalAuth: ExternalAuthentication = {
        provider: user.provider,
        idToken: user.idToken
      }
      this.validateExternalAuth(externalAuth);
    })
  }


  validateControl = (controlName: string) => {
    return this.loginForm?.get(controlName)?.invalid && this.loginForm.get(controlName)?.touched
  }

  validateExternalAuth(externalAuth: ExternalAuthentication) {
    this.authService.ExternalLogin(externalAuth)
      .subscribe({
        next: (response: AuthResponse) => {
          const token = response.token;
          this.localStorageService.set("jwt", token);
          this.authService.sendAuthStateChangeNotification(true);
          this.router.navigateByUrl("workoutSession");
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      })
  }

  hasError = (controlName: string, errorName: string) => {
    return this.loginForm?.get(controlName)?.hasError(errorName)
  }

  LoginUser(loginForm: FormGroup): void {
    this.showError = false;
    const credentials: UserForAuthentication = {
      userName: loginForm.get('userName')?.value,
      password: loginForm.get('password')?.value,
    }

    if (loginForm.valid) {
      this.authService.Login(credentials)
        .subscribe({
          next: (response: AuthResponse) => {
            const token = response.token;
            this.localStorageService.set("jwt", token);
            this.authService.sendAuthStateChangeNotification(true);
            this.router.navigateByUrl("workoutSession");
          },
          error: (err: HttpErrorResponse) => {
            this.errorMessage = err.message;
            this.showError = true;
          }
        })
    }
  }

}
