import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { SpiritDispenserSetting } from '../types/spirit-dispenser-setting';

const API_URL = environment.webApiBaseUrl + 'spiritdispenser'

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

  public async referenceDrive(): Promise<void>{
    const url = `${API_URL}/reference-drive`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async fillGlas(): Promise<void>{
    const url = `${API_URL}/fill-glas`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async goToBottleChangePosition(): Promise<void>{
    const url = `${API_URL}/goto-bottle-change`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async goToHomeposition(): Promise<void>{
    const url = `${API_URL}/goto-home-position`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async goToReleasePosition(): Promise<void>{
    const url = `${API_URL}/goto-release-position`;
    const result = await this.http.post(url, null).toPromise();
  }
}
