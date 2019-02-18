import { Pipe, PipeTransform, ElementRef, ViewChild } from '@angular/core';
import { ChangeDetectionStrategy, Component, OnInit, HostListener } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { Location } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent implements OnInit {
  @ViewChild(CdkVirtualScrollViewport) view: CdkVirtualScrollViewport;

  title = 'app';
  book: Book = null;
  bookId: string = null;
  pagesObserver = new BehaviorSubject<any[]>([]);
  screenHeight: string;
  screenWidth: number;  
  displayHeight: number;
  displayWidth: number;

  constructor(private location: Location, private router: Router, private httpClient: HttpClient) {
    router.events.subscribe((val) => {
      var path = location.path();
      if (path != '') {
        var id = path.substring(path.lastIndexOf('/') + 1);
        if (this.bookId == id) { return; }

        this.bookId = id;
        httpClient.get('/api/Comic/' + id).subscribe(x => this.onSelected(x);
      } else {
        var pages: string[] = [];
        this.bookId = null;
        this.book = null;
        this.pagesObserver.next(pages);
      }
    });
  }

  ngOnInit() {

    this.onResize({ target: window });
  }

  @HostListener('window:resize', ['$event'])
  onResize(event?) {    
    this.screenHeight = (event.target.innerHeight - 66) + "px";
    this.screenWidth = event.target.innerWidth;

    if (this.book) {
      this.displayHeight = (this.screenWidth / this.book.width) * this.book.height;
      this.displayWidth = this.screenWidth - 40;
    }
  }

  onSelected($event: any) {
    var pages = Array.from({ length: $event.length - 1 }).map((_, i) => `api/Image/${$event.id}/${i + 1}`);
    this.book = $event;
    this.pagesObserver.next(pages);

    this.displayHeight = (this.screenWidth / this.book.width) * this.book.height;
    this.displayWidth = this.screenWidth - 40;

    this.location.go("/comic/" + this.book.id);

    this.view.elementRef.nativeElement.focus();
    this.view.elementRef.nativeElement.scrollTop = 0;
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
