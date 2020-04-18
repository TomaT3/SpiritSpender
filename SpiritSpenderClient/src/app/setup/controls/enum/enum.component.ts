import { Component, OnInit, Input } from '@angular/core';
import { DrivingDirection } from 'src/app/setup/types/drive-setting';

@Component({
  selector: 'app-enum',
  templateUrl: './enum.component.html',
  styleUrls: ['./enum.component.scss']
})
export class EnumComponent implements OnInit {
  @Input() title: string;
  @Input() value: DrivingDirection;
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

  public descriptionsAndValues(): EnumDescriptionAndValue[]{
    const descriptions = Object.keys(DrivingDirection).filter(k => typeof DrivingDirection[k as any] === "number");
    const values = descriptions.map(k => new EnumDescriptionAndValue(DrivingDirection[k as any], k));
    return values;
  }
}

export class EnumDescriptionAndValue{
  value: number;
  viewValue: string

  constructor(value: any, viewValue: string){
    this.value = value;
    this.viewValue = viewValue;
  }
}
