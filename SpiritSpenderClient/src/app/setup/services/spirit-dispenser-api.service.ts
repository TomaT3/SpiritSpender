import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { SpiritDispenserSetting } from '../types/spirit-dispenser-setting';

const API_URL = environment.baseUrl + 'spiritdispenser'

@Injectable({
  providedIn: 'root'
})
export class SpiritDispenserApiService {

  constructor(private http: HttpClient) { }

  public async getSetting(): Promise<SpiritDispenserSetting> {
    const url = `${API_URL}/setting`;
    const setting = await this.http.get<SpiritDispenserSetting>(url).toPromise();
    return setting;
  }

  public async updateSettings(setting: SpiritDispenserSetting): Promise<void>{
    const url = `${API_URL}/setting`;
    const result = await this.http.put(url, setting).toPromise();
  }

  public async fillGlas(): Promise<void>{
    const url = `${API_URL}/fill-glas`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async closeSpiritSpender(): Promise<void>{
    const url = `${API_URL}/close-spirit-spender`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async openSpiritSpender(): Promise<void>{
    const url = `${API_URL}/release-spirit`;
    const result = await this.http.post(url, null).toPromise();
  }
}
