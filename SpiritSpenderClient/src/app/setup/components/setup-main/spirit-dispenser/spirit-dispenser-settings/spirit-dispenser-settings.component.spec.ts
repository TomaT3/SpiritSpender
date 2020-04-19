import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpiritDispenserSettingsComponent } from './spirit-dispenser-settings.component';

describe('SpiritDispenserSettingsComponent', () => {
  let component: SpiritDispenserSettingsComponent;
  let fixture: ComponentFixture<SpiritDispenserSettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpiritDispenserSettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpiritDispenserSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
