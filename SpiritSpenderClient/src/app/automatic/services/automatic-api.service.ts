import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const API_URL = environment.baseUrl + 'automatic'

@Injectable({
  providedIn: 'root'
})
export class AutomaticApiService {

  constructor(private http: HttpClient) { }

  public async releaseTheSpirit(): Promise<void> {
    const url = `${API_URL}/release-the-spirit`;
    const result = await this.http.post(url, null).toPromise();
  }

  public async referenceAllAxis(): Promise<void> {
    const url = `${API_URL}/reference-all-axis`;
    const result = await this.http.post(url, null).toPromise();
  }
}
