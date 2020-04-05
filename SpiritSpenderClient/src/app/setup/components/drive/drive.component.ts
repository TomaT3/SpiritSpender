import { Component, OnInit, Input } from '@angular/core';
import { DrivesApiService } from '../../services/drives-api.service';
import { Observable } from 'rxjs';
import { DriveSetting } from '../../types/drive-setting';
import { Angle, AngleUnits, Length, LengthUnits } from 'unitsnet-js';

@Component({
  selector: 'app-drive',
  templateUrl: './drive.component.html',
  styleUrls: ['./drive.component.scss']
})
export class DriveComponent implements OnInit {
  @Input() driveName: string;

  public driveSetting: DriveSetting;

  constructor(private drivesApiService: DrivesApiService) { }

  async ngOnInit(): Promise<void> {
    this.driveSetting = await this. drivesApiService.getDriveSetting(this.driveName);
  }

}
