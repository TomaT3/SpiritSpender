import { EventEmitter, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

const API_URL = environment.signalRBaseUrl

@Injectable({
  providedIn: 'root'
})
export class NcCommunicationSignalRService {
  messageReceived = new EventEmitter<string>();

  private connectionEstablished = new EventEmitter<Boolean>();  
  private _hubConnection: HubConnection;  

  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(API_URL + 'nc-communication')
      .build();

      this._hubConnection.onclose((error => {
        console.log('Hub connection closed: ' + error);
        this.tryReconnect();
      }))
  }  

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log(err);
        console.log('Error while establishing hub connection, retrying...');
        this.tryReconnect();
      });
  }

  private tryReconnect(): void {
    setTimeout(() => { this.startConnection(); }, 5000);
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('MessageReceived', (data: string) => {
      this.messageReceived.emit(data);
    });
  }
}
