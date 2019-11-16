import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'cbi-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  isHandset: boolean = false;
  componentActive: boolean = true;
  linkUrlPrefix: string;
  selectedClientId: number = 0;

  constructor(private breakpointObserver: BreakpointObserver ) {
    this.breakpointObserver
      .observe(Breakpoints.Handset)
      .subscribe((state: BreakpointState) => {
        if (state.matches) {
          this.isHandset = true;
        }
        else {
          this.isHandset = false;
        }
      });
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }
}
