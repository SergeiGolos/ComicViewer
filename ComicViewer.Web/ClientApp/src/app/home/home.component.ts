import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { filter, debounceTime, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: []
})
export class HomeComponent implements OnInit {
  private keyEvent = new Subject<string>()
  private books = [];
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit(): void {
    var http = this.httpClient;
    var test = function (n) {
      return http.get("/api/Comic/" + n);
    };

    this.keyEvent.pipe(
      filter(n => n.length > 3),
      debounceTime(300),
      switchMap(test)
    ).subscribe(n => {
      this.books = n as any[];
      console.log(n);
    });
  }

  

  onKey(event: any) {
    this.keyEvent.next(event.target.value);
  }
}
