import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MaxSpeedComponent } from './max-speed.component';

describe('MaxSpeedComponent', () => {
  let component: MaxSpeedComponent;
  let fixture: ComponentFixture<MaxSpeedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MaxSpeedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MaxSpeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
