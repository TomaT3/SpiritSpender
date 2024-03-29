import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StatusLampComponent } from './status-lamp.component';

describe('StatusLampComponent', () => {
  let component: StatusLampComponent;
  let fixture: ComponentFixture<StatusLampComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StatusLampComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusLampComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
