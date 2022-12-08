import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingformComponent } from './bookingform.component';
import { BookingRoutingModule } from '../booking-routing/booking-routing.module';
/*import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';*/


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    BookingRoutingModule  ,
   /* MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDatepickerModule,
    MatInputModule ,
    MatNativeDateModule,*/
  ]
})
export class BookingformModule { }
