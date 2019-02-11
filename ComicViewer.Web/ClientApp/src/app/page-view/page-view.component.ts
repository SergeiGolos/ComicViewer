import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-page-view',
  templateUrl: './page-view.component.html',
  styleUrls: ['./page-view.component.css']
})
export class PageViewComponent implements OnInit, OnDestroy {    
    index: string;
    id: string;
    updateEvent : Subscription
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get("id");
    this.index = this.route.snapshot.paramMap.get("index");
    this.updateEvent = this.route.paramMap.subscribe(params => {
      this.id = params.get("id");
      this.index = params.get("index");
    });

    
  }

  
  ngOnDestroy(): void {
    this.updateEvent.unsubscribe();
  }
}
