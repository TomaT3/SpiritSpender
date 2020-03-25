import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

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

}
