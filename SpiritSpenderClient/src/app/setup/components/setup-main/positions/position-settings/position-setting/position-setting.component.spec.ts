import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PositionSettingComponent } from './position-setting.component';

describe('PositionSettingComponent', () => {
  let component: PositionSettingComponent;
  let fixture: ComponentFixture<PositionSettingComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PositionSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PositionSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
