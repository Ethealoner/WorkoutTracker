import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkoutSessionDetailsComponent } from './workout-session-details.component';

describe('WorkoutSessionDetailsComponent', () => {
  let component: WorkoutSessionDetailsComponent;
  let fixture: ComponentFixture<WorkoutSessionDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WorkoutSessionDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WorkoutSessionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
