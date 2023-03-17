import { Component, OnInit, Input, Output, EventEmitter, NgZone, ViewChild, ElementRef, ViewEncapsulation  } from '@angular/core';
import { UnitsType } from 'src/app/shared/types/units-type';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import {take} from 'rxjs/operators';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UnitsComponent implements OnInit {
  @Input() title: string;
  @Input() autoSizeWidthToInput: boolean = false;
  @Input() get unitsParameter(): UnitsType{
    return this._unitsParameter;
  }

  set unitsParameter(value: UnitsType) {
    this._unitsParameter = value;
    this.unitsParameterChange.emit(this._unitsParameter);
  }
  @Input() editable: boolean;
  @Output() unitsParameterChange = new EventEmitter<UnitsType>();
  
  private _unitsParameter?: UnitsType;

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

  public getValue(): number {
    if(this.unitsParameter === undefined || this.unitsParameter === null)
      return -1;
    
    return this.unitsParameter.Value
  }

  public getUnit() {
    if(this.unitsParameter === undefined || this.unitsParameter === null)
      return "";

    return this.unitsParameter.Unit
  }
  
}
