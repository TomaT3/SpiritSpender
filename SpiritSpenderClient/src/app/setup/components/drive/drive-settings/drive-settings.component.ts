import { Component, OnInit, Input } from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import { DriveSetting, DriveSettingTexts } from 'src/app/setup/types/drive-setting';
import { LengthUnits, AccelerationUnits, SpeedUnits, Length, Speed } from 'unitsnet-js';

@Component({
  selector: 'app-drive-settings',
  templateUrl: './drive-settings.component.html',
  styleUrls: ['./drive-settings.component.scss']
})
export class DriveSettingsComponent implements OnInit {
  @Input() driveSetting: DriveSetting;

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

  constructor() { }

  ngOnInit(): void {
    Object.assign(this.driveSetting, this.currentDriveSetting);
    this.currentDriveSetting.stepsPerRevolution = 999;
  }


  public speed(): number {
    const string = this.driveSetting.maxSpeed.Value;
    //const value1 = name(this.driveSetting.maxSpeed.Value);
    if(string !== undefined){
      // const length1 = new Speed(23.4);
      // console.log(length1);
      // console.log(this.driveSetting.maxSpeed);
      // console.log(string);
      return string;
    }
      
      else return -1;
  }
}
