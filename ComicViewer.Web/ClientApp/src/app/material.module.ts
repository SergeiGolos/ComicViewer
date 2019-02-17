import { NgModule } from '@angular/core';
import { ScrollingModule } from '@angular/cdk/scrolling';
import {
  MatIconModule,
  MatToolbarModule,
  MatInputModule,
  MatAutocompleteModule,
  MatButtonModule,
  MatFormFieldModule
} from '@angular/material';

@NgModule({
  imports: [  
    MatButtonModule,
    MatToolbarModule,
    ScrollingModule ,
    MatInputModule,
    MatIconModule,
    MatAutocompleteModule,
    MatFormFieldModule
  ],
  exports: [
    MatButtonModule,
    MatToolbarModule,
    ScrollingModule,
    MatInputModule,
    MatIconModule,
    MatAutocompleteModule,
    MatFormFieldModule
  ]
})
export class MaterialModule { }
