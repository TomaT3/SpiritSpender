import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomaticActionsComponent } from './automatic-actions.component';

describe('AutomaticActionsComponent', () => {
  let component: AutomaticActionsComponent;
  let fixture: ComponentFixture<AutomaticActionsComponent>;

  beforeEach(async(() => {
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
