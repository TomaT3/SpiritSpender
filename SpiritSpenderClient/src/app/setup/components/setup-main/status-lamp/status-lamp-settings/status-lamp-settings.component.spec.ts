import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StatusLampSettingsComponent } from './status-lamp-settings.component';

describe('StatusLampSettingsComponent', () => {
  let component: StatusLampSettingsComponent;
  let fixture: ComponentFixture<StatusLampSettingsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StatusLampSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusLampSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
