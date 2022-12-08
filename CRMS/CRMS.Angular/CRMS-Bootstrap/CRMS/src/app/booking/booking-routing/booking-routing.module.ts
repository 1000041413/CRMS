import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BookingComponent } from '../booking.component';
import { AuthenticationGuardService } from '../../core/authentication/authentication-guard.service' 
import { BookingformComponent } from '../bookingform/bookingform.component';

const routes: Routes = [   
  { path: 'form', canActivate: [AuthenticationGuardService], component: BookingformComponent, data: { title: 'Booking Form' } },
  { path: '', canActivate: [AuthenticationGuardService], component: BookingComponent, data: { title: 'Booking' } }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class BookingRoutingModule { }

