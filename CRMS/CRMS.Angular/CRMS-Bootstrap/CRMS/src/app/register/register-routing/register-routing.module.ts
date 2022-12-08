import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from '../../register/register.component';

const routes: Routes = [
   { path: 'login', component: RegisterComponent, data: { title: 'Login' } },
   { path: 'register', component: RegisterComponent, data: { title: 'Register' } }];
@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class RegisterRoutingModule { }
