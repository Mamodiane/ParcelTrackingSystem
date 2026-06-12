export interface Parcel {
  id: number;
  trackingNumber: string;
  senderName: string;
  receiverName: string;
  pickupAddress: string;
  deliveryAddress: string;
  weight: number;
  status: string;
  createdDate: string;
}