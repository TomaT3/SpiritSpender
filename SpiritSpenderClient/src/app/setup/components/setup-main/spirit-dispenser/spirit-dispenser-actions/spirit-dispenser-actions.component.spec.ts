import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpiritDispenserActionsComponent } from './spirit-dispenser-actions.component';

describe('SpiritDispenserActionsComponent', () => {
  let component: SpiritDispenserActionsComponent;
  let fixture: ComponentFixture<SpiritDispenserActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpiritDispenserActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpiritDispenserActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
