import { Component, OnInit, Input } from '@angular/core';
import { OverlayType } from '../models/overlay-type';

@Component({
  selector: 'cbi-overlay-container',
  templateUrl: './overlay-container.component.html',
  styleUrls: ['./overlay-container.component.css']
})
export class OverlayContainerComponent implements OnInit {
  @Input() message: string;
  @Input() type: OverlayType;
  loaderType = OverlayType;

  constructor() { }

  ngOnInit() {
  }

}
