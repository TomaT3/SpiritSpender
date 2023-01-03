import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { UnitstypeOneActionComponent } from './unitstype-one-action.component';

describe('UnitstypeOneActionComponent', () => {
  let component: UnitstypeOneActionComponent;
  let fixture: ComponentFixture<UnitstypeOneActionComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ UnitstypeOneActionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitstypeOneActionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
