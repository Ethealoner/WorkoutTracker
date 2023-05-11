import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { WorkoutSessionDetails } from '../../Models/workout-session.model';
import { WorkoutSessionService } from '../../Services/workout-session.service';
import { LocalStorageService } from '../../Services/local-storage.service';
import { ExerciseService } from '../../Services/exercise.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-workout-session-details',
  templateUrl: './workout-session-details.component.html',
  styleUrls: ['./workout-session-details.component.css']
})
export class WorkoutSessionDetailsComponent implements OnInit {

  @Input() workoutSessionDetails?: WorkoutSessionDetails;
  errorMessage: string = '';
  showError: boolean = false;

  constructor(private workoutSessionService: WorkoutSessionService, private route: ActivatedRoute,
    private location: Location, private localStorageService: LocalStorageService, private router: Router,
    private exerciseService: ExerciseService) { }

  ngOnInit(): void {
    this.GetWorkoutSessionDetails();
  }

  GetWorkoutSessionDetails(): void {
    const workoutSessionId = this.route.snapshot.paramMap.get('id');
    if (workoutSessionId) {
      this.workoutSessionService.GetWorkoutSessionDetails(workoutSessionId).subscribe(workoutDetails => this.workoutSessionDetails = workoutDetails);
    }
  }

  goBack(): void {
    this.location.back();
  }

  AddExercies(): void {
    const workoutSessionId = this.route.snapshot.paramMap.get('id');
    if (workoutSessionId) {
      this.localStorageService.saveExerciseKey(null);
      this.localStorageService.saveWorkoutSessionKey(workoutSessionId);
      this.router.navigateByUrl('exercise');
      return;
    }

    this.errorMessage = "Unable to retrieve workout session ID";
    this.showError = true;
  }

  EditExercise(exerciseId: number): void {
    const workoutSessionId = this.route.snapshot.paramMap.get('id');
    if (workoutSessionId) {
      this.localStorageService.saveWorkoutSessionKey(workoutSessionId);
      this.localStorageService.saveExerciseKey(exerciseId);
      this.router.navigateByUrl('exercise');
      return;
    }
    this.errorMessage = "Unable to retrieve workout session ID";
    this.showError = true;
  }

  DeleteExercise(exerciseId: number): void {
    const workoutSessionId = this.route.snapshot.paramMap.get('id');
    if (workoutSessionId) {
      this.exerciseService.DeleteExercise(workoutSessionId, exerciseId).subscribe({
        next: (data) => {
          this.ngOnInit();
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      });
      return;
    }
    this.errorMessage = "Unable to retrieve workout session ID";
    this.showError = true;

  }
    
}
