import { Component, OnInit, Input } from '@angular/core';
import {MatInputModule} from '@angular/material/input';
import { DriveSetting } from 'src/app/setup/types/drive-setting';
import { LengthUnits, AccelerationUnits, SpeedUnits, Length, Speed } from 'unitsnet-js';

@Component({
  selector: 'app-drive-settings',
  templateUrl: './drive-settings.component.html',
  styleUrls: ['./drive-settings.component.scss']
})
export class DriveSettingsComponent implements OnInit {
  @Input() driveSetting: DriveSetting;

  constructor() { }

  ngOnInit(): void {
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
