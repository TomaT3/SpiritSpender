import { Component, OnInit, Input } from '@angular/core';
import { DrivesApiService } from 'src/app/setup/services/drives-api.service';
import { UnitsType, LengthUnit } from 'src/app/setup/types/units-type';

@Component({
  selector: 'app-drive-actions',
  templateUrl: './drive-actions.component.html',
  styleUrls: ['./drive-actions.component.scss']
})
export class DriveActionsComponent implements OnInit {
  @Input() driveName: string;
  
  driveToPositionValue : UnitsType;
  driveDistanceValue: UnitsType;
  positionToSetValue : UnitsType;

  constructor(private drivesApiService: DrivesApiService) { }

  ngOnInit(): void {
    this.driveToPositionValue = new LengthUnit();
    this.driveDistanceValue = new LengthUnit();
    this.positionToSetValue = new LengthUnit();
  }

  public async driveToPosition(position: UnitsType) : Promise<void> {
    await this.drivesApiService.driveToPosition(this.driveName, position);
  }

  public async driveDistanceMinus(distance: UnitsType) : Promise<void> {
    const distanceToGo = new LengthUnit();
    distanceToGo.Value = distance.Value * -1;
    await this.drivesApiService.driveDistance(this.driveName, distanceToGo);
  }

  public async driveDistancePlus(distance: UnitsType) : Promise<void> {
    await this.drivesApiService.driveDistance(this.driveName, distance);
  }

  public async setPosition(positionToSet: UnitsType) : Promise<void> {
    await this.drivesApiService.setPosition(this.driveName, positionToSet);
  }

  public async referenceDrive() : Promise<void> {
    await this.drivesApiService.referenceDrive(this.driveName);
  }
}
