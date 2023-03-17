import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-setup-main',
  templateUrl: './setup-main.component.html',
  styleUrls: ['./setup-main.component.scss']
})
export class SetupMainComponent implements OnInit {
  public drives: string[];

  constructor() { }

  async ngOnInit(): Promise<void> {
  }
}
