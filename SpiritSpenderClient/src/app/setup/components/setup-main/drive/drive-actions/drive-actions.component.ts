import { Component, OnInit, Input } from '@angular/core';
import { DrivesApiService } from 'src/app/setup/services/drives-api.service';

@Component({
  selector: 'app-drive-actions',
  templateUrl: './drive-actions.component.html',
  styleUrls: ['./drive-actions.component.scss']
})
export class DriveActionsComponent implements OnInit {
  @Input() driveName: string;
  
  constructor(private drivesApiService: DrivesApiService) { }

  ngOnInit(): void {
  }

}
