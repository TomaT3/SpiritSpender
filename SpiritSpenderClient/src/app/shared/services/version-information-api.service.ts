import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { VersionInformation } from '../types/versioin-information';

const API_URL = environment.baseUrl + 'VersionInformation'

@Injectable({
  providedIn: 'root'
})
export class VersionInformationApiService {

  constructor(private http: HttpClient) { }

  public async getVersionInformation(): Promise<VersionInformation> {
    const url = `${API_URL}`;
    const version = await this.http.get<VersionInformation>(url).toPromise();
    return version;
  }
}
