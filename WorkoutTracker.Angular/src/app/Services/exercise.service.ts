import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Exercise } from '../Models/exercise.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService {

  baseApiUrl: string = environment.baseApiUrl;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private htpp: HttpClient, private localStorageService: LocalStorageService) { }

  GetExercise(workoutSessionKey: any, id: number): Observable<Exercise>{
    return this.htpp.get<Exercise>(this.baseApiUrl + `api/exercise/${workoutSessionKey}/${id}`);
  }

  AddExercise(exercise: Exercise): Observable<Exercise> {
    return this.htpp.post<Exercise>(this.baseApiUrl + `api/exercise`, exercise, this.httpOptions);
  }

  DeleteExercise(workoutSessionKey: any, id: number): Observable<Exercise> {
    return this.htpp.delete<Exercise>(this.baseApiUrl + `api/exercise/${workoutSessionKey}/${id}`)
  }

  UpdateExercise(exercise: Exercise): Observable<Exercise> {
    return this.htpp.put<Exercise>(this.baseApiUrl + `api/exercise/`, exercise, this.httpOptions);
  }
}
