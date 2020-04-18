import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-number',
  templateUrl: './number.component.html',
  styleUrls: ['./number.component.scss']
})
export class NumberComponent implements OnInit {
  @Input() title: string;
  @Input() get value(): number {
    return this._value;
  }
  @Input() editable: boolean;
  @Output() valueChange = new EventEmitter<number>();

  set value(newValue: number) {
    this._value = newValue;
    this.valueChange.emit(this._value);
  }

  _value: number;
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
    this.value = $event.target.value;
  }
  
}
