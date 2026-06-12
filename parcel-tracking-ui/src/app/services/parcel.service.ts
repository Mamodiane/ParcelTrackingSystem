import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Parcel } from '../models/parcel';

@Injectable({
  providedIn: 'root'
})
export class ParcelService {

  private apiUrl = 'http://localhost:5289/api/Parcels';

  constructor(private http: HttpClient) {}

  getParcels(): Observable<Parcel[]> {
    return this.http.get<Parcel[]>(this.apiUrl);
  }

  getParcelById(id: number): Observable<Parcel> {
    return this.http.get<Parcel>(`${this.apiUrl}/${id}`);
  }

  createParcel(parcel: Partial<Parcel>): Observable<Parcel> {
    return this.http.post<Parcel>(this.apiUrl, parcel);
  }

  markAsCollected(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/collect`, {});
  }

  markAsInTransit(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/in-transit`, {});
  }

  markAsDelivered(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/deliver`, {});
  }

  deleteParcel(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getParcelByTrackingNumber(trackingNumber: string): Observable<Parcel> {
  return this.http.get<Parcel>(
    `${this.apiUrl}/track/${trackingNumber.trim()}`
  );
  }
}