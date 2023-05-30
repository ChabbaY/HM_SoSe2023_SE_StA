import { Component } from '@angular/core';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-datenschutz',
  templateUrl: './datenschutz.component.html',
  styleUrls: ['./datenschutz.component.scss']
})
export class DatenschutzComponent {
  constructor(private viewportScroller: ViewportScroller) {

  }

  scroll(elementId: string): void {
    this.viewportScroller.scrollToAnchor(elementId);
  }
}
