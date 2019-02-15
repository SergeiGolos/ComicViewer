import { ChangeDetectionStrategy, Component, OnInit, HostListener } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent implements OnInit{
    title = 'app';
    book = {};
    pagesObserver = new BehaviorSubject<any[]>([]);
    screenHeight: string;
    screenWidth: number;
    pageHeight = 1600;
    pageWidth = null;
  
  ngOnInit() {

    this.onResize({ target: window });
  }

    @HostListener('window:resize', ['$event'])
    onResize(event?) {
      this.screenHeight = (event.target.innerHeight - 66) + "px";
      this.screenWidth = event.target.innerWidth;
    }

    onSelected($event: any) {
      var pages = Array.from({ length: $event.numberOfPages - 1 }).map((_, i) => `api/Image/${$event.id}/${i + 1}`);
      this.book = $event;
      this.pagesObserver.next(pages);
    }
}
