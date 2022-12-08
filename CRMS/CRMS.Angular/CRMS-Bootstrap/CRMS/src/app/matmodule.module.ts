import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatAutocompleteModule } from '@angular/material/autocomplete'; 
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { MatListModule} from '@angular/material/list';
import {MatInputModule}from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import{MatToolbarModule}from '@angular/material/toolbar';
@NgModule({
  imports:
   [MatTableModule,
    MatPaginatorModule,
    MatSlideToggleModule,
    MatDatepickerModule,
    MatAutocompleteModule, MatCardModule, MatListModule,MatInputModule,MatSelectModule,MatToolbarModule],
  exports:
  [MatTableModule,
   MatPaginatorModule,
   MatSlideToggleModule,
   MatDatepickerModule,
   MatAutocompleteModule, MatCardModule, MatListModule,MatInputModule,MatSelectModule,MatToolbarModule],
  declarations: []
})
export class MatmoduleModule { }
