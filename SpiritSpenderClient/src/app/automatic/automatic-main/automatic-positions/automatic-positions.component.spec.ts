import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AutomaticPositionsComponent } from './automatic-positions.component';

describe('AutomaticPositionsComponent', () => {
  let component: AutomaticPositionsComponent;
  let fixture: ComponentFixture<AutomaticPositionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomaticPositionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomaticPositionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
