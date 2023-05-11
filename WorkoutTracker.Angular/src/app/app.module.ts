import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './Components/login/login.component';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './auth.guard';
import { WorkoutSessionComponent } from './Components/workout-session/workout-session.component';
import { RegisterComponent } from './Components/register/register.component';
import { ErrorHandlerService } from './Services/error-handler.service';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from '@angular/router';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { WorkoutSessionDetailsComponent } from './Components/workout-session-details/workout-session-details.component';
import { ExerciseComponent } from './Components/Exercise/exercise.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { IconsModule } from './icons/icons.module';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    WorkoutSessionComponent,
    RegisterComponent,
    WorkoutSessionDetailsComponent,
    ExerciseComponent,
  ],
  imports: [
    BrowserModule, RouterModule, HttpClientModule, AppRoutingModule, FormsModule, ReactiveFormsModule, SocialLoginModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:7250", "workouttrackerwebapi.azurewebsites.net"],
        disallowedRoutes: []
      }
    }),
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    IconsModule,
  ],
  providers: [AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlerService, multi: true },
    {
      provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: false,
        providers: [{
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider("1068052464614-d229pn67tt4ej4pv0521phg6ob0pce3d.apps.googleusercontent.com", {
            scopes: "email"
          })
        }],
        onError: (err: any) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig
    },
],
  bootstrap: [AppComponent]
})
export class AppModule { }
