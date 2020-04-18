import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnitstypeTwoActionsComponent } from './unitstype-two-actions.component';

describe('UnitstypeTwoActionsComponent', () => {
  let component: UnitstypeTwoActionsComponent;
  let fixture: ComponentFixture<UnitstypeTwoActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnitstypeTwoActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitstypeTwoActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
