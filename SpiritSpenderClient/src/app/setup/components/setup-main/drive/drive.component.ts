import { Component, OnInit, Input } from '@angular/core';
import { DrivesApiService } from '../../../services/drives-api.service';
import { Observable } from 'rxjs';
import { DriveSetting } from '../../../types/drive-setting';
import { Angle, AngleUnits, Length, LengthUnits } from 'unitsnet-js';
import { UnitsType } from '../../../../shared/types/units-type';
import { AxisSignalRService } from 'src/app/shared/services/signal-r/axis-signal-r.service';
import { PositionDto } from 'src/app/shared/types/position-dto';

@Component({
  selector: 'app-drive',
  templateUrl: './drive.component.html',
  styleUrls: ['./drive.component.scss']
})
export class DriveComponent implements OnInit {
  @Input() driveName: string = "";
  public currentPosition: UnitsType;

  constructor(private drivesApiService: DrivesApiService, private axisSignalRservice: AxisSignalRService) { }

  async ngOnInit(): Promise<void> {
    // this.currentPosition = await this.drivesApiService.getCurrentPosition(this.driveName);
    // this.axisSignalRservice.positionChanged.subscribe((position: PositionDto) => this.positionChangedHandler(position));
  }

  // ngOnDestroy() {
  //   this.axisSignalRservice?.positionChanged.unsubscribe();
  // }

  // private positionChangedHandler(position: PositionDto){
  //   if(this.driveName == position.axisName)
  //   {
  //     this.currentPosition = position.positon
  //   }
  // }
}
