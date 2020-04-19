import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpiritDispenserComponent } from './spirit-dispenser.component';

describe('SpiritDispenserComponent', () => {
  let component: SpiritDispenserComponent;
  let fixture: ComponentFixture<SpiritDispenserComponent>;

  beforeEach(async(() => {
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
