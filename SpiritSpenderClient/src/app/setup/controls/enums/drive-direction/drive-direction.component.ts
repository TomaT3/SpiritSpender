import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { DrivingDirection } from 'src/app/setup/types/drive-setting';

@Component({
  selector: 'app-drive-direction',
  templateUrl: './drive-direction.component.html',
  styleUrls: ['./drive-direction.component.scss']
})
export class DriveDirectionComponent implements OnInit {
  @Input() title: string;
  @Input() get value(): DrivingDirection {
    return this._value;
  }
  @Input() editable: boolean;
  @Output() valueChange = new EventEmitter<DrivingDirection>();

  set value(newValue: DrivingDirection) {
    this._value = newValue;
    this.valueChange.emit(this._value);
  }

  private _value: DrivingDirection;

  public drivingDirections = DrivingDirection;
  constructor() { }

  ngOnInit(): void {
  }

  public get opacity(): number{
    if(this.editable)
      return 1;
    else
      return 0.5;
  }
}
