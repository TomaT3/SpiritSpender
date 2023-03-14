import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NcCommunicationComponent } from './nc-communication.component';

describe('NcCommunicationComponent', () => {
  let component: NcCommunicationComponent;
  let fixture: ComponentFixture<NcCommunicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NcCommunicationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NcCommunicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
