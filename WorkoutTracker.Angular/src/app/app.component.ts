import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'WorkoutTracker Angular';
  isUserAuthenticated?: boolean;

  constructor(private router: Router, private authService: AuthService) {

  }

  ngOnInit(): void {
    this.isUserAuthenticated = this.authService.isUserAuthenticated();
    this.authService.authChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      })
  }

  logout(): void {
    this.authService.Logout();

    if (this.authService.isExternalAuth)
      this.authService.signOutExternal();

    this.router.navigateByUrl('/login');
  }

}
