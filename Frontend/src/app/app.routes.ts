import { Routes } from '@angular/router';
import { RoomComponent } from './pages/room/room';
import { Customer } from './pages/customer/customer';
import { Booking } from './pages/booking/booking';
import { HeaderComponent } from './components/header/header.component';

export const routes: Routes = [
  { path: '', component: HeaderComponent },
  { path: 'customers', component: Customer },
  { path: 'bookings', component: Booking },
  { path: 'rooms', component: RoomComponent },
];
