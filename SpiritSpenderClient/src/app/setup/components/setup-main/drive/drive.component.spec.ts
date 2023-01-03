import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DriveComponent } from './drive.component';

describe('DriveComponent', () => {
  let component: DriveComponent;
  let fixture: ComponentFixture<DriveComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DriveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DriveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
