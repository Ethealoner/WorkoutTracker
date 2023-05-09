import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService implements HttpInterceptor {

  constructor(private router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = this.handleError(error);
          return throwError(() => new Error(errorMessage));
        })
      )
  }

  private handleError = (error: HttpErrorResponse): string => {
    if (error.status === 404) {
      return this.handleNotFound(error);
    }
    else if (error.status === 400) {
      return this.handleBadRequest(error);
    }
    else if (error.status === 401) {
      return this.handleUnauthorized(error);
    }
    return 'Unknown Error';
  }

  private handleNotFound = (error: HttpErrorResponse): string => {
    return error.message;
  }

  private handleBadRequest = (error: HttpErrorResponse): string => {
    if (this.router.url === 'register') {
      let message = '';
      const values = Object.values(error.error.errors);
      values.map((m) => {
        message += m + '<br>';
      })

      return message.slice(0, -4);
    }
    else {
      return error.error ? error.error : error.message;
    }
  }

  private handleUnauthorized = (error: HttpErrorResponse): string => {
    if ((this.router.url === '/login' || this.router.url === '/')) {
      return "Authentication failed. Wrong Username or Password";
    }
    else {
      this.router.navigate(['login']);
      return error.message
    }
  }

}
