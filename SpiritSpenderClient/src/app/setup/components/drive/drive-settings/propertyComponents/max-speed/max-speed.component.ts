import { Component, OnInit, Input } from '@angular/core';
import { UnitsType } from 'src/app/setup/types/units-type';

@Component({
  selector: 'app-max-speed',
  templateUrl: './max-speed.component.html',
  styleUrls: ['./max-speed.component.scss']
})
export class MaxSpeedComponent implements OnInit {
  @Input() title: string;
  @Input() maxSpeedParameter: UnitsType;
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
