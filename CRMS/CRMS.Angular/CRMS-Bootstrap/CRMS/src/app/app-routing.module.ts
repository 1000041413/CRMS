import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BookingformComponent } from './booking/bookingform/bookingform.component';
import { RegisterComponent } from './register/register.component';
import { RoomformComponent } from './room/roomform/roomform.component';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'booking',
    loadChildren: () => import('./booking/booking.module').then(m => m.BookingModule)
  },
 
  
  {
    path: 'room',
    loadChildren: () => import('./room/room.module').then(m => m.RoomModule)
  },  
   {
    path: '',
    redirectTo: 'Register',
    pathMatch: 'full'
  },
  { path: '**', redirectTo: 'login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
