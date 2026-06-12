import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Parcel } from '../../models/parcel';
import { ParcelService } from '../../services/parcel.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-parcels',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './parcels.component.html',
  styleUrl: './parcels.component.css'
})
export class ParcelsComponent implements OnInit {
  parcels: Parcel[] = [];

  newParcel = {
    senderName: '',
    receiverName: '',
    pickupAddress: '',
    deliveryAddress: '',
    weight: 0
  };

  trackingNumberSearch: string = '';
  trackParcel: Parcel | null = null;

  constructor(private parcelService: ParcelService) {}

  ngOnInit(): void {
    this.loadParcels();
  }

  loadParcels(): void {
    this.parcelService.getParcels().subscribe(data => {
      this.parcels = data;
    });
  }

createParcel(): void {
  this.parcelService.createParcel(this.newParcel).subscribe(() => {
    this.loadParcels();
    this.newParcel = { 
      senderName: '', 
      receiverName: '', 
      pickupAddress: '', 
      deliveryAddress: '', 
      weight: 0 };
  });
}

  markCollected(id: number): void {
  this.parcelService.markAsCollected(id).subscribe(() => {
    this.loadParcels();
  });
}

markInTransit(id: number): void {
  this.parcelService.markAsInTransit(id).subscribe(() => {
    this.loadParcels();
  });
}

markDelivered(id: number): void {
  this.parcelService.markAsDelivered(id).subscribe(() => {
    this.loadParcels();
  });
}

deleteParcel(id: number): void {
  this.parcelService.deleteParcel(id).subscribe(() => {
    this.loadParcels();
  }); 
}

searchByTrackingNumber(): void {
  console.log('Button clicked');
  console.log('Tracking number:', this.trackingNumberSearch);

  if (!this.trackingNumberSearch.trim()) {
    alert('Please enter a tracking number');
    return;
  }

  this.parcelService
    .getParcelByTrackingNumber(this.trackingNumberSearch)
    .subscribe({
      next: (parcel) => {
        console.log('Parcel found:', parcel);
        this.trackParcel = parcel;
      },
      error: (error) => {
        console.error('Search error:', error);
        this.trackParcel = null;
        alert('Parcel not found');
      }
    });
}

//dashboard
get totalParcels(): number {
  return this.parcels.length; 
}
get collectedParcels(): number {
  return this.parcels.filter(p => p.status === 'Collected').length;
}
get inTransitParcels(): number {
  return this.parcels.filter(p => p.status === 'In Transit').length;
}
get deliveredParcels(): number {
  return this.parcels.filter(p => p.status === 'Delivered').length;
}

get pendingParcels(): number {
  return this.parcels.filter(p => p.status === 'Pending').length;
}

}