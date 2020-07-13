import { Component, OnInit, Input } from '@angular/core';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { RouterOutlet } from '@angular/router';
import { slideInAnimation } from './nav-animations';
import { SidenavItem } from './models/side-nav-item';
import { AppFeatures } from '../gateway-api/models';

@Component({
  selector: 'cbi-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  animations: [
    slideInAnimation
  ]
})
export class NavComponent implements OnInit {
  @Input() appFeatures: AppFeatures;
  isHandset: boolean = false;
  componentActive: boolean = true;
  linkUrlPrefix: string;
  selectedClientId: number = 0;
  menuItems: SidenavItem[] = [];

  ngOnInit(): void {
    this.renderMenu();    
  }

  renderMenu(){
    this.menuItems = [ 
      {
        id: "route-home",
        label: 'Home',
        routerLink: "/home",
        enabled: true,
      },
      {
        id: "route-customer-orders",
        label: 'Customer Orders',
        routerLink: "/customer-orders",
        enabled: this.appFeatures.customerOrdersView.enabled
      },
      {
        id: "route-customer-orders-ngrx",
        label: 'Customer Orders (NGRX)',
        routerLink: "/customer-orders-ngrx",
        enabled: this.appFeatures.customerOrdersView.enabled
      }];
  }

  constructor(private breakpointObserver: BreakpointObserver) {
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
