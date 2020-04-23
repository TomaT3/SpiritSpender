import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { UnitsType } from '../../../shared/types/units-type';

@Component({
  selector: 'app-unitstype-two-actions',
  templateUrl: './unitstype-two-actions.component.html',
  styleUrls: ['./unitstype-two-actions.component.scss']
})
export class UnitstypeTwoActionsComponent implements OnInit {
  @Input() title: string;
  @Input() action1Text: string;
  @Input() action2Text: string;
  @Input() get unitsValue(): UnitsType {
    return this._unitsValue;
  }
  set unitsValue(newValue: UnitsType) {
    this._unitsValue = newValue;
    this.unitsValueChange.emit(this._unitsValue);
  }
  @Output() action1Clicked = new EventEmitter<UnitsType>();
  @Output() action2Clicked = new EventEmitter<UnitsType>();
  @Output() unitsValueChange = new EventEmitter<UnitsType>();

  private _unitsValue: UnitsType;
  constructor() { }

  ngOnInit(): void {
  }

  public button1Clicked(): void {
    this.action1Clicked.emit(this.unitsValue);
  }

  public button2Clicked(): void {
    this.action2Clicked.emit(this.unitsValue);
  }
}
