import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.css']
})
export class PublishersComponent implements OnInit {
  publishersObserver = new BehaviorSubject<any[]>([]);
  constructor(private httpClient: HttpClient, private router: Router) { }

  ngOnInit() {
    this.httpClient.get('/api/Comic/publishers').subscribe((event: any[]) => {
      this.publishersObserver.next(event);
    });
  }

  onClickPublish(name: string) {
    this.router.navigateByUrl('publisher/' + name);
  }

 
}
