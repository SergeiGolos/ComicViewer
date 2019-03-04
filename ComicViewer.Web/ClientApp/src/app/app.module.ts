import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { AppComponent, Base64ImagePipe } from './app.component';
import { SearchComponent  } from './search/search.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module/material.module';
import { PublishersComponent } from './publishers/publishers.component';
import { PublisherComponent } from './publisher/publisher.component';
import { ComicComponent } from './comic/comic.component';

@NgModule({
  declarations: [
    AppComponent,    
    SearchComponent,            
    Base64ImagePipe,
    PublishersComponent,
    PublisherComponent,
    ComicComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      { path: 'comic/:id', component: ComicComponent },
      { path: 'publisher/:name', component: PublisherComponent },
      { path: '', component: PublishersComponent  }
    ]),
    HttpClientModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule
  ],
  providers: [Location, { provide: LocationStrategy, useClass: PathLocationStrategy }],
  bootstrap: [AppComponent]
})
export class AppModule { }
