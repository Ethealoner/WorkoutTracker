import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { UserForRegistration } from '../../Models/user.model';
import { AuthService } from '../../Services/auth.service';
import { PasswordConfirmationValidatorService } from '../../Services/password-confirmation-validator.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  public errorMessage: string = '';
  public showError: boolean = false;

  constructor(private authService: AuthService, private passService: PasswordConfirmationValidatorService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup(
      {
        userName: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('')
      });
    this.registerForm.get('confirmPassword')?.setValidators([Validators.required,
    this.passService.validateConfirmPassowrd(this.registerForm.get('password'))]);
  }

  validateControl(controlName: string) {
    return this.registerForm.get(controlName)?.invalid && this.registerForm.get(controlName)?.touched;
  }

  hasError(controlName: string, errorName: string) {
    return this.registerForm.get(controlName)?.hasError(errorName);
  }

  registerUser() {
    this.showError = false;
    const user: UserForRegistration = {
      UserName: this.registerForm.get('userName')?.value,
      Password: this.registerForm.get('password')?.value,
      ConfirmPassword: this.registerForm.get('confirmPassword')?.value
    }
    console.log(user);
    this.authService.Register(user)
      .subscribe({
        next: () => console.log("Yey"),
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
          console.log(err.error.errors)
        }
      })
  }

}
