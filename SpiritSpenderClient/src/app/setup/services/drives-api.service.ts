
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DriveSetting } from '../types/drive-setting';

const API_URL = 'http://localhost:5000/api/drives'

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
    // const driveSetting1 = this.http.get(url).pipe(
    //   map(x => x));
    const driveSetting = await this.http.get<DriveSetting>(url).toPromise();
    return driveSetting;
  }

}
