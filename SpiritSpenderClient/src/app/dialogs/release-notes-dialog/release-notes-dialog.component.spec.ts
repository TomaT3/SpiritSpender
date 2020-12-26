import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReleaseNotesDialogComponent } from './release-notes-dialog.component';

describe('ReleaseNotesDialogComponent', () => {
  let component: ReleaseNotesDialogComponent;
  let fixture: ComponentFixture<ReleaseNotesDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReleaseNotesDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReleaseNotesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
