
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DriveSetting } from '../types/drive-setting';

const API_URL = 'http://localhost:32769/api/drives'

@Injectable({
  providedIn: 'root'
})
export class DrivesApiService {

  constructor(private http: HttpClient) { }

  public getAllDrives(): Observable<string[]> {
    const drives = this.http.get<string[]>(API_URL);
    return drives;
  }

  public getDriveSetting(driveName: string): Observable<DriveSetting> {
    const url = `${API_URL}/${driveName}/setting`;
    // const driveSetting1 = this.http.get(url).pipe(
    //   map(x => x));
    const driveSetting = this.http.get<DriveSetting>(url);
    return driveSetting;
  }

}
