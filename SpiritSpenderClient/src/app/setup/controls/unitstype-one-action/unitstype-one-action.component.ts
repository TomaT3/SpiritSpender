import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { UnitsType } from '../../types/units-type';

@Component({
  selector: 'app-unitstype-one-action',
  templateUrl: './unitstype-one-action.component.html',
  styleUrls: ['./unitstype-one-action.component.scss']
})
export class UnitstypeOneActionComponent implements OnInit {
  @Input() title: string;
  @Input() actionText: string;
  @Input() get unitsValue(): UnitsType {
    return this._unitsValue;
  }
  set unitsValue(newValue: UnitsType) {
    this._unitsValue = newValue;
    this.unitsValueChange.emit(this._unitsValue);
  }
  @Output() actionClicked = new EventEmitter<UnitsType>();
  @Output() unitsValueChange = new EventEmitter<UnitsType>();

  private _unitsValue: UnitsType;
  constructor() { }

  ngOnInit(): void {
  }

  public buttonClicked(): void {
    this.actionClicked.emit(this.unitsValue);
  }
}
