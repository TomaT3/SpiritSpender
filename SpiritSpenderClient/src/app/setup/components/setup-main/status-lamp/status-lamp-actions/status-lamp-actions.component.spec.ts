import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StatusLampActionsComponent } from './status-lamp-actions.component';

describe('StatusLampActionsComponent', () => {
  let component: StatusLampActionsComponent;
  let fixture: ComponentFixture<StatusLampActionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StatusLampActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusLampActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
