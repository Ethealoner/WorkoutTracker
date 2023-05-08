import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthResponse, UserForAuthentication } from '../Models/user.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient, private localStorageService: LocalStorageService) { }

  Login(credentials: UserForAuthentication): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.baseApiUrl + `api/authenticate/login`, credentials, this.httpOptions);
  }

  Logout(): void {
    this.localStorageService.remove("jwt");
  }

}
