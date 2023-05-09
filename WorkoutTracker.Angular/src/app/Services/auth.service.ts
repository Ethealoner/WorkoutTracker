import { GoogleLoginProvider, SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthResponse, ExternalAuthentication, UserForAuthentication, UserForRegistration } from '../Models/user.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  private extAuthChangeSub = new Subject<SocialUser>();
  public extAuthChanged = this.extAuthChangeSub.asObservable();

  public isExternalAuth: boolean = false;

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  constructor(private http: HttpClient, private localStorageService: LocalStorageService,
    private jwtHelper: JwtHelperService, private externalAuthService: SocialAuthService) {
    this.externalAuthService.authState.subscribe((user) => {
      console.log(user)
      this.extAuthChangeSub.next(user);
      this.isExternalAuth = true;
    })
  }

  public signOutExternal = () => {
    this.externalAuthService.signOut();
  }

  public isUserAuthenticated(): boolean {
    const token = this.localStorageService.get('jwt');
    if (!token)
      return false;

    return !this.jwtHelper.isTokenExpired(token);
  }


  sendAuthStateChangeNotification(isAuthenticated: boolean) {
    this.authChangeSub.next(isAuthenticated);
  }

  ExternalLogin(externalAuth: ExternalAuthentication) {
    return this.http.post<AuthResponse>(this.baseApiUrl + `api/authenticate/externalLogin`, externalAuth, this.httpOptions)
  }

  Login(credentials: UserForAuthentication): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.baseApiUrl + `api/authenticate/login`, credentials, this.httpOptions);
  }

  Logout(): void {
    this.localStorageService.remove("jwt");
    this.authChangeSub.next(false);
  }

  Register(credentials: UserForRegistration): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(this.baseApiUrl + `api/authenticate/register`, credentials, this.httpOptions);
  }

}
