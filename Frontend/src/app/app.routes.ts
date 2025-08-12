import { Routes } from '@angular/router';
import { RoomComponent } from './pages/room/room';
import { CustomerComponent } from './pages/customer/customer';
import { BookingComponenet } from './pages/booking/booking';
import { HeaderComponent } from './components/header/header.component';

export const routes: Routes = [
  { path: '', component: HeaderComponent },
  { path: 'customers', component: CustomerComponent },
  { path: 'bookings', component: BookingComponenet },
  { path: 'rooms', component: RoomComponent },
];
