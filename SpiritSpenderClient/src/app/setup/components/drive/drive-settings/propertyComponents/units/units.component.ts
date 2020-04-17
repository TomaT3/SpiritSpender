import { Component, OnInit, Input, Output, EventEmitter  } from '@angular/core';
import { UnitsType } from 'src/app/setup/types/units-type';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.scss']
})
export class UnitsComponent implements OnInit {
  @Input() title: string;
  @Input() get unitsParameter(): UnitsType{
    return this._unitsParameter;
  }

  set unitsParameter(value: UnitsType) {
    this._unitsParameter = value;
    this.unitsParameterChange.emit(this._unitsParameter);
  }
  @Input() editable: boolean;
  @Output() unitsParameterChange = new EventEmitter<UnitsType>();

  private _unitsParameter: UnitsType;

  constructor() { }

  ngOnInit(): void {
  }

  public get opacity(): number{
    if(this.editable)
      return 1;
    else
      return 0.5;
  }

  public valueChanged($event): void{
    this.unitsParameter.Value = $event.target.value;
    this.unitsParameter = this.unitsParameter;
  }
}
