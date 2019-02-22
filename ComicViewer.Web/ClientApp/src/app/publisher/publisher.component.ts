import { Component, OnInit, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.css']
})
export class PublisherComponent implements OnInit {
  skip = 0;
  take = 20;
  screenHeight = "";
  screenWidth = "";
  list: any[] = [];
  publisher: string;
  publisherObserver = new BehaviorSubject<any[]>([]);
  constructor(private httpClient: HttpClient, private router: Router, private location: Location) { }

  ngOnInit() {
    this.onResize({ target: window });
    var path = this.location.path();
    if (path != '') {
      this.publisher = path.substring(path.lastIndexOf('/') + 1);
      this.load();
    }
  }

  load() {
    this.httpClient.get(`/api/Comic/publisher/${this.publisher}?skip=${this.skip}&take=${this.take}`).subscribe((event: any[]) => {
      for (var i = 0; i < event.length; i++) {
        this.list.push(event[i]);
      }
      this.publisherObserver.next(this.list);
      this.skip = this.skip + this.take;
    });
  }

  onScroll(event) {    
    this.load();
  }
  @HostListener('window:resize', ['$event'])
  onResize(event?) {
    this.screenHeight = (event.target.innerHeight - 66) + "px";
    this.screenWidth = event.target.innerWidth + "px";
  }

}
