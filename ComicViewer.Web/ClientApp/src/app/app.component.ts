import { Pipe, PipeTransform } from '@angular/core';
import { ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';
import { Location } from '@angular/common';
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent implements OnInit {

  constructor(private location: Location, private router: Router) {}
  ngOnInit(): void {
       //  throw new Error("Method not implemented.");
  }

  onSelected($event: any) {    
    this.router.navigateByUrl("/comic/" + $event.id);
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

