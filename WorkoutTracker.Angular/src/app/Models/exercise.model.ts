
export interface ExerciseSummary {
  exerciseName: string;
  exerciseId: number;
}

export interface Exercise {
  exerciseName: string;
  exerciseId: number;
  workoutSessionId: string;
  typeOfExercise: TypeOfExercise;
  repetitions: string;
}

export enum TypeOfExercise {
  Weigth = 'Weigth',
  Cardio = 'Cardio'
}

export interface RepsForm {
  Repetitions: number;
  Difficulty: number;
}
