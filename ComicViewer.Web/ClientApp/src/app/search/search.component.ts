import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { filter, debounceTime, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  providers: []
})
export class SearchComponent implements OnInit{      
  @Output() onSelected: EventEmitter<any> = new EventEmitter();
  @ViewChild('search') input; 
  searchCtrl = new FormControl();
  searchEvent: Observable<Object>;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    var http = this.httpClient;
    this.searchEvent = this.searchCtrl.valueChanges
      .pipe(
        filter(n => n.length > 2),
        debounceTime(300),
        switchMap(n => http.get(`/api/Comic/Find/${n}?skip=0&take=20`))
      );
  }

  displayFn(book): string | undefined {
    return book ? [book.name, book.title, book.volume, book.issue].join(" ") : undefined;
  }

  onFocus(event) {
    this.input.nativeElement.select();
  }

  onSearchSelected(event) {
    this.onSelected.emit(event.source.value);    
  }
}
