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

  private connectionEstablished = new EventEmitter<Boolean>();  
  private _hubConnection: HubConnection;  


  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(API_URL + 'axis')
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
    this._hubConnection.on('PositionChanged', (data: PositionDto) => {
      console.log(`X: ${data?.X?.Value}  Y: ${data?.Y?.Value}`)
      this.positionChanged.emit(data);
    });
  }
}
