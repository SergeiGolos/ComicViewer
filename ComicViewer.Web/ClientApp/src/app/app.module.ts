import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent, Base64ImagePipe } from './app.component';
import { SearchComponent  } from './search/search.component';
import { PageViewComponent } from './page-view/page-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule, MatInputModule, MatAutocompleteModule, MatButtonModule, MatFormFieldModule } from '@angular/material';
import { ScrollingModule } from '@angular/cdk/scrolling';

@NgModule({
  declarations: [
    AppComponent,    
    SearchComponent,        
    PageViewComponent,
    Base64ImagePipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    ScrollingModule ,
    MatInputModule,
    MatAutocompleteModule,
    MatFormFieldModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
