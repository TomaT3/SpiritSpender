import { Component, OnInit } from '@angular/core';
import { NcCommunicationApiService } from 'src/app/shared/services/nc-communication-api.service';
import { NcCommunicationSignalRService } from 'src/app/shared/services/signal-r/nc-communication-signal-r.service';

@Component({
  selector: 'app-nc-communication',
  templateUrl: './nc-communication.component.html',
  styleUrls: ['./nc-communication.component.scss']
})
export class NcCommunicationComponent implements OnInit {

  ncCommand: string = '';
  ncMessages: string ='';
  
  constructor(private ncCommunicationApiService: NcCommunicationApiService, private ncCommunicationSignalRService: NcCommunicationSignalRService) { }

  async ngOnInit(): Promise<void> {
    this.ncCommunicationSignalRService.messageReceived.subscribe((mesage: string) => this.messageReceivedHandler(mesage));
  }

  ngOnDestroy() {
    //this.ncCommunicationSignalRService?.messageReceived.unsubscribe();
  }

  public sendNcCommand(): void {
    this.ncCommunicationApiService.sendNcCommand(this.ncCommand)
    this.ncCommand = '';
  }

  private messageReceivedHandler(ncMessage: string): void {
    this.ncMessages += ncMessage; // + "\r\n";
  }

}
