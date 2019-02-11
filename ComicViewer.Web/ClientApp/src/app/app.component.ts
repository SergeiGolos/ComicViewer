import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  title = 'app';
  book = {};
  pages = [];

  onSelected($event: any) {
    this.book = $event;
    this.pages = Array.from({ length: $event.numberOfPages-1 }).map((_, i) => `api/Image/${$event.id}/${i+1}`);
  }
}
