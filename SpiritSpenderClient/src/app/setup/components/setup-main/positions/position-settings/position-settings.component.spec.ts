import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PositionSettingsComponent } from './position-settings.component';

describe('PositionSettingsComponent', () => {
  let component: PositionSettingsComponent;
  let fixture: ComponentFixture<PositionSettingsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PositionSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
