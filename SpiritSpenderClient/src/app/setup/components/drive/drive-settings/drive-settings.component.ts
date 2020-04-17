import { Component, OnInit, Input } from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import { DriveSetting, DriveSettingTexts } from 'src/app/setup/types/drive-setting';
import { LengthUnits, AccelerationUnits, SpeedUnits, Length, Speed } from 'unitsnet-js';
import { DrivesApiService } from 'src/app/setup/services/drives-api.service';
import { CopyHelper } from 'src/app/shared/Helpers/CopyHelper';

@Component({
  selector: 'app-drive-settings',
  templateUrl: './drive-settings.component.html',
  styleUrls: ['./drive-settings.component.scss']
})
export class DriveSettingsComponent implements OnInit {
  @Input() driveName: string; 
  public driveSetting: DriveSetting;
  public currentDriveSetting: DriveSetting;

  public readonly maxSpeed = DriveSettingTexts.maxSpeed;
  public readonly stepsPerRevolution = DriveSettingTexts.stepsPerRevolution;
  public readonly spindlePitch = DriveSettingTexts.spindlePitch;
  public readonly acceleration = DriveSettingTexts.acceleration;
  public readonly softwareLimitMinus = DriveSettingTexts.softwareLimitMinus;
  public readonly softwareLimitPlus = DriveSettingTexts.softwareLimitPlus;
  public readonly reverseDirection = DriveSettingTexts.reverseDirection;
  public readonly referencePosition = DriveSettingTexts.referencePosition;
  public readonly referenceDrivingSpeed = DriveSettingTexts.referenceDrivingSpeed;
  public readonly referenceDrivingDirection = DriveSettingTexts.referenceDrivingDirection;

  constructor(private drivesApiService: DrivesApiService) { }

  async ngOnInit(): Promise<void> {
    if (this.driveName !== undefined)
    {
    this.driveSetting = await this.drivesApiService.getDriveSetting(this.driveName);
    this.currentDriveSetting = <DriveSetting>CopyHelper.deepCopy(this.driveSetting);
    }
  }


  public async updateValues(): Promise<void> {
    await this.drivesApiService.updateDriveSettings(this.driveName, this.currentDriveSetting);
    this.driveSetting = await this.drivesApiService.getDriveSetting(this.driveName);
  }

  
}
