import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SpiritDispenserComponent } from './spirit-dispenser.component';

describe('SpiritDispenserComponent', () => {
  let component: SpiritDispenserComponent;
  let fixture: ComponentFixture<SpiritDispenserComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SpiritDispenserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpiritDispenserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
