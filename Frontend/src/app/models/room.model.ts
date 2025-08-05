import { RoomType } from './roomtype.model';

export interface Room {
  id: number;
  number: string;
  description: string;
  capacity: number;
  pricePerNight: number;
  isAvailable: boolean;
  roomType: RoomType;
}
