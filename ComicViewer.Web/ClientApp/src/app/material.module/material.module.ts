import { NgModule } from '@angular/core';
import { ScrollingModule } from '@angular/cdk/scrolling';
import {
  MatIconModule,
  MatToolbarModule,
  MatInputModule,
  MatAutocompleteModule,
  MatButtonModule,
  MatFormFieldModule,
  MatGridListModule,
  MatCardModule
} from '@angular/material';

@NgModule({
  imports: [  
    MatButtonModule,
    MatToolbarModule,
    ScrollingModule ,
    MatInputModule,
    MatIconModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatGridListModule,
    MatCardModule
  ],
  exports: [
    MatButtonModule,
    MatToolbarModule,
    ScrollingModule,
    MatInputModule,
    MatIconModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    MatGridListModule,
    MatCardModule
  ]
})
export class MaterialModule { }
