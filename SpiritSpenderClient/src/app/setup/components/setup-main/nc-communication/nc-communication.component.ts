import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nc-communication',
  templateUrl: './nc-communication.component.html',
  styleUrls: ['./nc-communication.component.scss']
})
export class NcCommunicationComponent implements OnInit {

  value: string = '';
  constructor() { }

  ngOnInit(): void {
  }

  public sendNcCommand(): void {

  }

}
