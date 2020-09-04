import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatusLampSettingsComponent } from './status-lamp-settings.component';

describe('StatusLampSettingsComponent', () => {
  let component: StatusLampSettingsComponent;
  let fixture: ComponentFixture<StatusLampSettingsComponent>;

  beforeEach(async(() => {
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
