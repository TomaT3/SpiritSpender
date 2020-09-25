import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StatusLampSetting } from '../types/status-lamp-setting';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const API_URL = environment.baseUrl + 'statuslamp'

@Injectable({
  providedIn: 'root'
})
export class StatusLampApiService {

  constructor(private http: HttpClient) { }

  public async getSetting(): Promise<StatusLampSetting> {
    const url = `${API_URL}/setting`;
    const setting = await this.http.get<StatusLampSetting>(url).toPromise();
    return setting;
  }

  public async updateSettings(setting: StatusLampSetting): Promise<void>{
    const url = `${API_URL}/setting`;
    const result = await this.http.put(url, setting).toPromise();
  }

  public async enableStatusLamp(): Promise<void>{
    const url = `${API_URL}/enabled`;

    // HttpClient uses wrong content type for boolean. See: https://github.com/angular/angular/issues/38924
    const result = await this.http.post(url, true, {headers : new HttpHeaders({ 'Content-Type': 'application/json' })}).toPromise();
  }

  public async disableStatusLamp(): Promise<void>{
    const url = `${API_URL}/enabled`;

    // HttpClient uses wrong content type for boolean. See: https://github.com/angular/angular/issues/38924
    const result = await this.http.post(url, false, {headers : new HttpHeaders({ 'Content-Type': 'application/json' })}).toPromise();
  }
  
  public async redLightOff(): Promise<void>{
    const url = `${API_URL}/red-light/off`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async redLightOn(): Promise<void>{
    const url = `${API_URL}/red-light/on`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async redLightBlink(): Promise<void>{
    const url = `${API_URL}/red-light/blink`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async greenLightOff(): Promise<void>{
    const url = `${API_URL}/green-light/off`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async greenLightOn(): Promise<void>{
    const url = `${API_URL}/green-light/on`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async greenLightBlink(): Promise<void>{
    const url = `${API_URL}/green-light/blink`;
    const result = await this.http.post(url, null).toPromise();
  }
}
