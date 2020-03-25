import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-drive',
  templateUrl: './drive.component.html',
  styleUrls: ['./drive.component.scss']
})
export class DriveComponent implements OnInit {
  @Input() driveName: string;

  constructor() { }

  ngOnInit(): void {
  }

}
