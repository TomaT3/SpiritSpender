import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReleaseNotesDialogData } from './release-notes-dialog-data';

@Component({
  selector: 'app-release-notes-dialog',
  templateUrl: './release-notes-dialog.component.html',
  styleUrls: ['./release-notes-dialog.component.scss']
})
export class ReleaseNotesDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public dialogData: ReleaseNotesDialogData) { }

  ngOnInit(): void {
  }

}
