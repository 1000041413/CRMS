import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from '../dashboard.component';
import { AuthenticationGuardService } from '../../core/authentication/authentication-guard.service';

const routes: Routes = [
  { path: '', canLoad: [AuthenticationGuardService], component: DashboardComponent, data: { title: 'Booking' } }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class DashboardRoutingModule { }
