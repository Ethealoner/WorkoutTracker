import { ExerciseSummary } from "./exercise.model";

export interface WorkoutSession {
  workoutSessionId: string;
  workoutScore: number;
  workoutDate: string;
  userId: string;
}

export interface AddNewWorkoutSession {
  userId: string;
  createdAt: string;
}

export interface WorkoutSessionDetails {
  workoutSessionId: string;
  createdAt: string;
  workoutSessionScore: number;
  exerciseSummaries: ExerciseSummary[]
}
