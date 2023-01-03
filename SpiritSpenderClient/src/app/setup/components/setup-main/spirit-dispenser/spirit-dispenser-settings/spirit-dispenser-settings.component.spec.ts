import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SpiritDispenserSettingsComponent } from './spirit-dispenser-settings.component';

describe('SpiritDispenserSettingsComponent', () => {
  let component: SpiritDispenserSettingsComponent;
  let fixture: ComponentFixture<SpiritDispenserSettingsComponent>;

  beforeEach(waitForAsync(() => {
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
