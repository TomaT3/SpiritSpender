import { Component, OnInit, Input } from '@angular/core';
import { DrivesApiService } from '../../services/drives-api.service';
import { Observable } from 'rxjs';
import { DriveSetting } from '../../types/drive-setting';
import { Angle, AngleUnits, Length, LengthUnits } from 'unitsnet-js';
import { UnitsType } from '../../types/units-type';

@Component({
  selector: 'app-drive',
  templateUrl: './drive.component.html',
  styleUrls: ['./drive.component.scss']
})
export class DriveComponent implements OnInit {
  @Input() driveName: string = "";
  public currentPosition: UnitsType;

  constructor(private drivesApiService: DrivesApiService) { }

  async ngOnInit(): Promise<void> {
    //this.currentPosition = await this.drivesApiService.getCurrentPosition(this.driveName);
  }
}
