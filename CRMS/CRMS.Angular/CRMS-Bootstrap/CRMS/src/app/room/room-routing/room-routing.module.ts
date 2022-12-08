import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoomComponent } from '../room.component';
import { AuthenticationGuardService } from '../../core/authentication/authentication-guard.service';
import { RoomformComponent } from '../roomform/roomform.component';

const routes: Routes = [
  { path: 'form', canActivate: [AuthenticationGuardService], component: RoomformComponent, data: { title: 'Room Form' } },
  { path: '', canActivate: [AuthenticationGuardService], component: RoomComponent, data: { title: 'Manage Rooms' } }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class RoomRoutingModule { }
