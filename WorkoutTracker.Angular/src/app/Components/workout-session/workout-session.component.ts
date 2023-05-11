import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AddNewWorkoutSession, WorkoutSession } from '../../Models/workout-session.model';
import { LocalStorageService } from '../../Services/local-storage.service';
import { WorkoutSessionService } from '../../Services/workout-session.service';

@Component({
  selector: 'app-workout-session',
  templateUrl: './workout-session.component.html',
  styleUrls: ['./workout-session.component.css'],
  providers: [DatePipe]
})
export class WorkoutSessionComponent implements OnInit {

  workoutSessions: WorkoutSession[] = [];
  currentDate = new Date();
  errorMessage: string = '';
  showError: boolean = false;

  constructor(private localStorageService: LocalStorageService, private workoutSessionService: WorkoutSessionService,
              private datePipe: DatePipe  ) {
    this.datePipe.transform(this.currentDate, 'yyyy-MM-dd');
  }

  ngOnInit(): void {
    const userId = this.localStorageService.getUserId();
    if (userId !== null) {
      this.workoutSessionService.GetUsersWorkoutSessions(userId)
        .subscribe({
          next: (workoutSessionList) => {
            this.workoutSessions = workoutSessionList;
          },
          error: (err: HttpErrorResponse) => {
            this.errorMessage = err.message;
            this.showError = true;
          }
        });
    }
  }

  addWorkoutSession(): void {
    const userId = this.localStorageService.getUserId();
    if (userId !== null) {
      var addWorkoutSession: AddNewWorkoutSession = {
        createdAt: this.currentDate.toISOString(),
        userId: userId
      };
      this.workoutSessionService.AddWorkoutSession(addWorkoutSession)
        .subscribe({
          next: (data) => {
            this.ngOnInit();
          },
          error: (err: HttpErrorResponse) => {
            this.errorMessage = err.message;
            this.showError = true;
          }
        });
    }
  }

  deleteWorkoutSession(id: string): void {
    this.workoutSessionService.DeleteWorkoutSession(id)
      .subscribe({
        next: (data) => {
          this.ngOnInit();
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      }
      );
      
  }
}
