import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ReleaseNotesDialogComponent } from './release-notes-dialog.component';

describe('ReleaseNotesDialogComponent', () => {
  let component: ReleaseNotesDialogComponent;
  let fixture: ComponentFixture<ReleaseNotesDialogComponent>;

  beforeEach(waitForAsync(() => {
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
