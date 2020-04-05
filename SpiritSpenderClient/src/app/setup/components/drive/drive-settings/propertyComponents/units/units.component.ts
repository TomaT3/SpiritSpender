import { Component, OnInit, Input } from '@angular/core';
import { UnitsType } from 'src/app/setup/types/units-type';

@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.scss']
})
export class UnitsComponent implements OnInit {
  @Input() title: string;
  @Input() unitsParameter: UnitsType;
  @Input() editable: boolean;

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
