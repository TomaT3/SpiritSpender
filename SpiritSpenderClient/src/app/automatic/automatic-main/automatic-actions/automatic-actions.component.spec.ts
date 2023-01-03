import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AutomaticActionsComponent } from './automatic-actions.component';

describe('AutomaticActionsComponent', () => {
  let component: AutomaticActionsComponent;
  let fixture: ComponentFixture<AutomaticActionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomaticActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomaticActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
