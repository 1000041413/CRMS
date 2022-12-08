import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../../login/login.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [ { path: '', component: LoginComponent, data: { title: 'Login' } }];
@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class LoginRoutingModule { }
