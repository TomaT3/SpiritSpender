import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { INcCommand, NcCommand } from '../types/nc-command';

const API_URL = environment.webApiBaseUrl + 'NcCommunication'

@Injectable({
  providedIn: 'root'
})
export class NcCommunicationApiService {

  constructor(private http: HttpClient) { }

  public async sendNcCommand(ncCommandString: string): Promise<void> {
    const url = `${API_URL}/send-command`;
    const ncCommand = new NcCommand(ncCommandString);
    const retVal = await this.http.post<INcCommand>(url, ncCommand).toPromise();
  }
}
