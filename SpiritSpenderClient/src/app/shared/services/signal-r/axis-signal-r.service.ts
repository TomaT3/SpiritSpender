import { EventEmitter, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { PositionDto } from '../../types/position-dto';

const API_URL = environment.signalRBaseUrl

@Injectable({
  providedIn: 'root'
})
export class AxisSignalRService {
  positionChanged = new EventEmitter<PositionDto>();  
  connectionEstablished = new EventEmitter<Boolean>();  
  
  private connectionIsEstablished = false;  
  private _hubConnection: HubConnection;  
  connection: signalR.HubConnection;


  constructor() {
    this.createConnection();  
    this.registerOnServerEvents();  
    this.startConnection();  
  }  
  
  private createConnection() {  
    this._hubConnection = new HubConnectionBuilder()  
      .withUrl(API_URL + 'axis')  
      .build();  
  }  
  
  private startConnection(): void {  
    this._hubConnection  
      .start()
      .then(() => {  
        this.connectionIsEstablished = true;  
        console.log('Hub connection started');  
        this.connectionEstablished.emit(true);  
      })  
      .catch(err => {  
        console.log('Error while establishing connection, retrying...');  
        this.connectionIsEstablished = false;
        setTimeout(function () { this.startConnection(); }, 5000);  
      });  
  }  
  
  private registerOnServerEvents(): void {  
    this._hubConnection.on('PositionChanged', (data: PositionDto) => {  
      this.positionChanged.emit(data);  
    });  
  }  
}
