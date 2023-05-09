import { Injectable } from '@angular/core';
import { AbstractControl, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PasswordConfirmationValidatorService {

  constructor() { }

  public validateConfirmPassowrd = (passwordControl: AbstractControl | null): ValidatorFn => {
    return (confirmationControl: AbstractControl): { [key: string]: boolean } | null => {
      const confirmValue = confirmationControl.value;
      const passowrdValue = passwordControl?.value;

      if (confirmValue === '') {
        return null;
      }

      if (confirmValue !== passowrdValue) {
        return { mustMatch: true };
      }

      return null;
    }
  }
}
