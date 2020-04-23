import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PositionActionsComponent } from './position-actions.component';

describe('PositionActionsComponent', () => {
  let component: PositionActionsComponent;
  let fixture: ComponentFixture<PositionActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PositionActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
