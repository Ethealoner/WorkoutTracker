import { Injectable } from '@angular/core';
import jwt_decode, { JwtPayload } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  workoutSessionKey!: string | null;
  exerciseKey!: number | null;

  set(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  get(key: string) {
    return localStorage.getItem(key);
  }

  remove(key: string) {
    localStorage.removeItem(key);
  }

  getUserId(): string | null {
    const token = localStorage.getItem('jwt');
    if (token === null)
      return null;

      const tokenInfo = jwt_decode<JwtPayload>(token);
      if (tokenInfo.sub != null)
        return tokenInfo.sub;
    return null;
    
  }

  saveWorkoutSessionKey(workoutSessionKey: string | null) {
    this.workoutSessionKey = workoutSessionKey;
  }

  getWorkoutSessionKey(): string | null {
    return this.workoutSessionKey;
  }

  saveExerciseKey(exerciseKey: number | null) {
    this.exerciseKey = exerciseKey;
  }

  getExerciseKey(): number | null {
    return this.exerciseKey;
  }
}
