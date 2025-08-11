import { Customer } from './customer.model';
import { Room } from './room.model';

export interface Booking {
  Id: number;
  Notes: string;
  Customer: Customer;
  Room: Room;
  CheckInDate: Date;
  CheckOutDate: Date;
  Status: string; // Possible values: Pending, Confirmed, Cancelled
  TotalPrice: number;
}
