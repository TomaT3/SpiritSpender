import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DriveActionsComponent } from './drive-actions.component';

describe('DriveActionsComponent', () => {
  let component: DriveActionsComponent;
  let fixture: ComponentFixture<DriveActionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DriveActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DriveActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
