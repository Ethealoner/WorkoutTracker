import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthResponse, UserForAuthentication } from '../../Models/user.model';
import { AuthService } from '../../Services/auth.service';
import { LocalStorageService } from '../../Services/local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  credentials: UserForAuthentication = { userName: '', password: '' };
  invalidLogin: boolean = false;

  constructor(
    private router: Router,
    private http: HttpClient,
    private authService: AuthService,
    private localStorageService: LocalStorageService)
  { }

  ngOnInit(): void {
  }

  LoginUser(loginForm: NgForm): void {
    if (loginForm.valid) {
      this.authService.Login(this.credentials)
        .subscribe({
          next: (response: AuthResponse) => {
            const token = response.token;
            this.localStorageService.set("jwt", token);
            this.invalidLogin = false;
            console.log("Success");
            this.router.navigateByUrl("/");
          },
          error: (err: HttpErrorResponse) => {
            this.invalidLogin = true;
            console.log(err);
          }
        })
    }
  }

}
