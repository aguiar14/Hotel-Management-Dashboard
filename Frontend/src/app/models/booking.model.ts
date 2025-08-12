import { Customer } from './customer.model';
import { Room } from './room.model';

export interface Booking {
  id: number;
  notes: string;
  customer: Customer;
  room: Room;
  checkInDate: Date;
  checkOutDate: Date;
  status: string; // Possible values: Pending, Confirmed, Cancelled
  totalPrice: number;
}
