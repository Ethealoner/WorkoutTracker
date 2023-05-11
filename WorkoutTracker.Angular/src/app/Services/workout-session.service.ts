import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { AddNewWorkoutSession, WorkoutSession, WorkoutSessionDetails } from '../Models/workout-session.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutSessionService {

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private htpp: HttpClient, private localStorageService: LocalStorageService) { }

  GetUsersWorkoutSessions(userId: string): Observable<WorkoutSession[]> {
    return this.htpp.get<WorkoutSession[]>(this.baseApiUrl + `api/workoutsession/${userId}`);
  }

  AddWorkoutSession(addWorkoutSession: AddNewWorkoutSession): Observable<AddNewWorkoutSession> {
    return this.htpp.post<AddNewWorkoutSession>(this.baseApiUrl + `api/workoutsession`, addWorkoutSession ,this.httpOptions);
  }

  DeleteWorkoutSession(sessionId: string): Observable<WorkoutSession> {
    return this.htpp.delete<WorkoutSession>(this.baseApiUrl + `api/workoutsession/${sessionId}`, this.httpOptions);
  }

  GetWorkoutSessionDetails(workoutSessionId: string): Observable<WorkoutSessionDetails> {
    return this.htpp.get<WorkoutSessionDetails>(this.baseApiUrl + `api/workoutSessionDetails/${workoutSessionId}`);
  }

}
