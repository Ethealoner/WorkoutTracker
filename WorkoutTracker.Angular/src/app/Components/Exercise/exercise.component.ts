import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Exercise, RepsForm, TypeOfExercise } from '../../Models/exercise.model';
import { ExerciseService } from '../../Services/exercise.service';
import { LocalStorageService } from '../../Services/local-storage.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})

export class ExerciseComponent implements OnInit {

  workoutSessionKey!: string;
  exercise?: Exercise
  exerciseTypes = TypeOfExercise;

  errorMessage: string = '';
  showError: boolean = false;

  exerciseTypesForms = new FormControl<TypeOfExercise>(TypeOfExercise.Weigth);

  exerciseRepetitionForms = this.formBuilder.group({
    reps: this.formBuilder.array([])
  });

  exerciseTypeLabel1?: string;
  exerciseTypeLabel2?: string;
  exerciseTypeLabel3?: string;

  constructor(private localStorageService: LocalStorageService, private router: Router,
    private formBuilder: FormBuilder, private exerciseService: ExerciseService, private location: Location) {
    const key = localStorageService.getWorkoutSessionKey();
    if (key !== null) {
      this.workoutSessionKey = key;
      console.log(this.workoutSessionKey);
      this.getExercise();
    }
    else {
      console.log("Unable to retireve key!");
      // Add better logging here
      router.navigateByUrl("workoutSession");
    }

  }

  ngOnInit(): void {

    this.exerciseTypesForms.valueChanges
      .subscribe(ch => {
        this.onExerciseTypeChanged(ch!);
      });
  }

  getExerciseTypes(obj: any) {
    return Object.keys(obj);
  }

  onExerciseTypeChanged(value: TypeOfExercise): void {
    if (this.exercise) {
      this.exercise.typeOfExercise = value;
      this.setExerciseLabels();
    }
  }

  getRepsFromForms(repsForm: RepsForm[]): void {
    if (this.exercise) {
      this.exercise.repetitions = JSON.stringify(repsForm);
    }
  }

  get reps() {
    return this.exerciseRepetitionForms.get('reps') as FormArray;
  }

  addExerciseRep(): void {
    const exerciseGroup = this.formBuilder.group({
      Repetitions: [1, Validators.required],
      Difficulty: [1, Validators.required]
    });

    this.reps.push(exerciseGroup);
  }

  recreateRepsInForms(): void {
    if (this.exercise) {
      let repsForm: RepsForm[];
      repsForm = JSON.parse(this.exercise.repetitions);
      for (let rep of repsForm) {
        let exerciseGroup = this.formBuilder.group({
          Repetitions: [rep.Repetitions, Validators.required],
          Difficulty: [rep.Difficulty, Validators.required]
        });
        this.reps.push(exerciseGroup);
      }
    }
  }

  getExercise(): void {
    const id = this.localStorageService.getExerciseKey();
    if (id != null) {
      // Exercise already exists - edit option
      this.exerciseService.GetExercise(this.workoutSessionKey, id).subscribe(exercise => {
        this.exercise = exercise;
        this.recreateRepsInForms();
        this.setExerciseLabels();
      })
    }
    else {
      this.exercise = {
        exerciseName: 'New Exercise',
        workoutSessionId: this.workoutSessionKey,
        exerciseId: 0,
        typeOfExercise: TypeOfExercise.Weigth,
        repetitions: '',
      }
    }

  }

  setExerciseLabels(): void {
    if (this.exercise) {
      if (this.exercise.typeOfExercise == TypeOfExercise.Weigth) {
        this.exerciseTypeLabel1 = "Rep";
        this.exerciseTypeLabel2 = "weight";
        this.exerciseTypeLabel3 = "KG";
      } else {
        this.exerciseTypeLabel1 = "Distance(M)";
        this.exerciseTypeLabel2 = "time";
        this.exerciseTypeLabel3 = "MIN";
      }
    }
  }

  save(): void {
    if (this.exercise) {
      this.getRepsFromForms(this.reps.value);
      const isAdd = this.exercise.exerciseId != 0 ? true : false;
      this.saveChanges(isAdd, this.exercise);
    }
  }

  saveChanges(isAdd: boolean, exercise: Exercise): void {
    if (!isAdd) {
      console.log("call add");
      this.exerciseService.AddExercise(exercise).subscribe({
        next: (data) => {
          this.router.navigateByUrl('workoutSessionDetail/' + exercise.workoutSessionId)
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      });
    }
    else {
      console.log("call update");
      this.exerciseService.UpdateExercise(exercise).subscribe({
        next: (data) => {
          this.router.navigateByUrl('workoutSessionDetail/' + exercise.workoutSessionId)
        },
        error: (err: HttpErrorResponse) => {
          this.errorMessage = err.message;
          this.showError = true;
        }
      });
    }
  }

  goBack(): void {
    this.location.back();
  }
  removeExerciseRep(repIndex: number): void {
    this.reps.removeAt(repIndex);
  }
}
