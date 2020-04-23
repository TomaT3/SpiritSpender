
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DriveSetting } from '../types/drive-setting';
import { environment } from 'src/environments/environment';
import { UnitsType } from '../../shared/types/units-type';

const API_URL = environment.baseUrl + 'drives'

@Injectable({
  providedIn: 'root'
})
export class DrivesApiService {

  constructor(private http: HttpClient) { }

  public async getAllDrives(): Promise<string[]> {
    const drives = await this.http.get<string[]>(API_URL).toPromise();
    return drives;
  }

  public async getDriveSetting(driveName: string): Promise<DriveSetting> {
    const url = `${API_URL}/${driveName}/setting`;
    const driveSetting = await this.http.get<DriveSetting>(url).toPromise();
    return driveSetting;
  }

  public async updateDriveSettings(driveName: string, driveSetting: DriveSetting): Promise<void>{
    const url = `${API_URL}/${driveName}/setting`;
    const result = await this.http.put(url, driveSetting).toPromise();
  }

  public async getCurrentPosition(driveName: string): Promise<UnitsType> {
    const url = `${API_URL}/${driveName}/current-position`;
    const currentPosition = await this.http.get<UnitsType>(url).toPromise();
    return currentPosition;
  }

  public async driveToPosition(driveName: string, position: UnitsType): Promise<void>{
    const url = `${API_URL}/${driveName}/drive-to-position`;
    const result = await this.http.post(url, position).toPromise();
  }

  public async driveDistance(driveName: string, distance: UnitsType): Promise<void>{
    const url = `${API_URL}/${driveName}/drive-distance`;
    const result = await this.http.post(url, distance).toPromise();
  }

  public async setPosition(driveName: string, positionToSet: UnitsType): Promise<void>{
    const url = `${API_URL}/${driveName}/set-position`;
    const result = await this.http.post(url, positionToSet).toPromise();
  }

  public async referenceDrive(driveName: string): Promise<void>{
    const url = `${API_URL}/${driveName}/reference-drive`;
    const result = await this.http.post(url, null).toPromise();
  }

}
