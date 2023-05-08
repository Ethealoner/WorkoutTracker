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

  constructor(private router: Router, private authService: AuthService) {

  }

  logout(): void {
    this.authService.Logout();
  }

}
