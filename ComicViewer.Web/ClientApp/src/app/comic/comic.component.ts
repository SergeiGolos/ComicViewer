import { Pipe, PipeTransform, ElementRef, ViewChild } from '@angular/core';
import { ChangeDetectionStrategy, Component, OnInit, HostListener } from '@angular/core';
import { FixedSizeVirtualScrollStrategy, VIRTUAL_SCROLL_STRATEGY } from '@angular/cdk/scrolling';
import { BehaviorSubject } from 'rxjs';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { CdkVirtualScrollViewport } from '@angular/cdk/scrolling';
import { Location, Time } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';


export class CustomVirtualScrollStrategy extends FixedSizeVirtualScrollStrategy {
  constructor() {
    super(50, 250, 500);
  }
}


@Component({
  selector: 'app-comic',
  templateUrl: './comic.component.html',
  styleUrls: ['./comic.component.css'],
  providers: [{ provide: VIRTUAL_SCROLL_STRATEGY, useClass: CustomVirtualScrollStrategy }]
})
export class ComicComponent implements OnInit {
  @ViewChild(CdkVirtualScrollViewport) view: CdkVirtualScrollViewport;

  title = 'app';
  book: Book = null;
  pages: BookPage[] = [];
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
        httpClient.get('/api/Comic/' + id).subscribe(x => this.onSelected(x));
      } else {
        var pages: string[] = [];
        this.bookId = null;
        this.book = null;
        this.pagesObserver.next(pages);
      }
    });
  }
  getUrl(page) {
    return `api/Image/${page.comicId}/${page.page}`;

  }
  onSelected($event: any) {

    

    this.book = $event.comic;
    this.pages = $event.pages;

    this.pagesObserver.next(this.pages);
       
    this.view.elementRef.nativeElement.focus();
    this.view.elementRef.nativeElement.scrollTop = 0;
  }

  ngOnInit() {

    this.onResize({ target: window });
  }

  @HostListener('window:resize', ['$event'])
  onResize(event?) {
    this.screenHeight = (event.target.innerHeight - 66) + "px";
    this.screenWidth = event.target.innerWidth;

    if (this.book) {
      this.displayHeight = 900;//(this.screenWidth / this.book.width) * this.book.height;E
      //this.displayWidth = //this.screenWidth - 40;
    }
  }
}


interface BookPage {
  id: number;
  comicId: string;
  page: number;
  fileNameMask: string;
  extentions: string;
  height: number;
  width: number;

  viewHeight: number;
  viewWidth: number;
}

interface Book {
  id: string;
  created: Date;
  originalName: string;
  path: string;
  extension: string;
  length: number;
  publisher: string;
  name: string;
  year: string;
  month: string;
  volume: string;
  issue: string;
  title: string;
}



