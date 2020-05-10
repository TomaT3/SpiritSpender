import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Quantity, Position, PositionSettting } from '../types/position-settings';

const API_URL = environment.baseUrl + 'ShotGlassPositions'

@Injectable({
  providedIn: 'root'
})
export class ShotGlassPositionsApiService {

  constructor(private http: HttpClient) { }

  public async getNumberOfPositions(): Promise<number> {
    const url = `${API_URL}/count`;
    const numberOfPositions = await this.http.get<number>(url).toPromise();
    return numberOfPositions;
  }

  public async getPositionSettings(): Promise<PositionSettting[]> {
    const url = `${API_URL}`;
    const positionSettings = await this.http.get<PositionSettting[]>(url).toPromise();
    return positionSettings;
  }

  public async getPosition(positionNumber: number): Promise<Position> {
    const url = `${API_URL}/${positionNumber}/position`;
    const position = await this.http.get<Position>(url).toPromise();
    return position;
  }

  public async clearPositions(): Promise<PositionSettting[]> {
    const url = `${API_URL}/clear`;
    return await this.http.put<PositionSettting[]>(url, {}).toPromise();
  }

  public async getQuantity(positionNumber: number): Promise<Quantity> {
    const url = `${API_URL}/${positionNumber}/quantity`;
    const position = await this.http.get<Quantity>(url).toPromise();
    return position;
  }

  public async updatePosition(positionNumber: number, newPosition: Position): Promise<Position> {
    const url = `${API_URL}/${positionNumber}/position`;
    const position = await this.http.put<Position>(url, newPosition).toPromise();
    return position;
  }

  public async updateQuantity(positionNumber: number, newQuantity: Quantity): Promise<Quantity> {
    const url = `${API_URL}/${positionNumber}/quantity`;
    const position = await this.http.put<Quantity>(url, newQuantity).toPromise();
    return position;
  }

  public async driveToPosition(positionNumber: number): Promise<void> {
    const url = `${API_URL}/${positionNumber}/drive-to-position`;
    const retVal = await this.http.post<Quantity>(url, null).toPromise();
  }
}
