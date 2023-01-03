import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DriveDirectionComponent } from './drive-direction.component';

describe('DriveDirectionComponent', () => {
  let component: DriveDirectionComponent;
  let fixture: ComponentFixture<DriveDirectionComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DriveDirectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DriveDirectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
