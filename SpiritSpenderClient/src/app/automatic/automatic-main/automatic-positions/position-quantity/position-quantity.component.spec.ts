import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PositionQuantityComponent } from './position-quantity.component';

describe('PositionQuantityComponent', () => {
  let component: PositionQuantityComponent;
  let fixture: ComponentFixture<PositionQuantityComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PositionQuantityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionQuantityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
