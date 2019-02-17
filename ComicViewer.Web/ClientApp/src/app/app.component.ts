import { Pipe, PipeTransform } from '@angular/core';
import { ChangeDetectionStrategy, Component, OnInit, HostListener } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent implements OnInit {
  title = 'app';
  book: Book = null;
  pagesObserver = new BehaviorSubject<any[]>([]);
  screenHeight: string;
  screenWidth: number;  
  displayHeight: number;
  displayWidth: number;

  ngOnInit() {

    this.onResize({ target: window });
  }

  @HostListener('window:resize', ['$event'])
  onResize(event?) {
    this.screenHeight = (event.target.innerHeight - 66) + "px";
    this.screenWidth = event.target.innerWidth;
  }

  pageSize(event?) {
    if (this.displayHeight == this.book.height) {
      this.displayHeight = (this.screenWidth / this.book.width) * this.book.height;
      this.displayWidth = this.screenWidth;
    }
    else {
      this.displayHeight = this.book.height;
      this.displayWidth = this.book.width;      
    }
  }

  onSelected($event: any) {
    var pages = Array.from({ length: $event.length - 1 }).map((_, i) => `api/Image/${$event.id}/${i + 1}`);
    this.book = $event;
    this.pagesObserver.next(pages);

    this.displayHeight = this.book.height;
    this.displayWidth = this.book.width; 
  }
}

/*
 * Raise the value exponentially
 * Takes an exponent argument that defaults to 1.
 * Usage:
 *   value | exponentialStrength:exponent
 * Example:
 *   {{ 2 | exponentialStrength:10 }}
 *   formats to: 1024
*/
@Pipe({ name: 'base64image' })
export class Base64ImagePipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }

  transform(value: string): SafeHtml {
    return this.sanitizer.bypassSecurityTrustUrl('data:image/jpg;base64, ' + value);
  }
}

interface Book {
  id: string;
  name: string;
  thumbnail: string;

  height: number;
  width: number;
}
