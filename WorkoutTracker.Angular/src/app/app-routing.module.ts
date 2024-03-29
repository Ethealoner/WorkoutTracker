import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { AuthGuard } from './auth.guard';
import { WorkoutSessionComponent } from './Components/workout-session/workout-session.component';
import { RegisterComponent } from './Components/register/register.component';
import { WorkoutSessionDetailsComponent } from './Components/workout-session-details/workout-session-details.component';
import { ExerciseComponent } from './Components/Exercise/exercise.component';


const routes: Routes = [
  { path: '', component: WorkoutSessionComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'workoutSession', component: WorkoutSessionComponent, canActivate: [AuthGuard] },
  { path: 'workoutSessionDetail/:id', component: WorkoutSessionDetailsComponent, canActivate: [AuthGuard] },
  { path: 'exercise', component: ExerciseComponent, canActivate: [AuthGuard] }

]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
