import { Component, OnInit, OnDestroy, } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Customer } from 'src/app/gateway-api/models';
import { Store, select } from '@ngrx/store';
import { filter, map } from 'rxjs/operators';
import { AppState } from 'src/app/store';
import { selectCustomer, clearSelectedOrder, loadCustomerOrders, clearAllOrders } from '../store/customer-order.actions';
import { allFeatures } from 'src/app/feature-management/store/feature.selectors';


@Component({
  selector: 'cbi-customer-orders-view',
  templateUrl: './customer-orders-view.component.html',
  styleUrls: ['./customer-orders-view.component.css']
})
export class CustomerOrdersViewComponent implements OnInit, OnDestroy {
  customerOrdersViewEnabled$: Observable<boolean>;
  
  componentActive: boolean = true;

  constructor(private store: Store<AppState>) { }

  ngOnInit() {
    this.customerOrdersViewEnabled$ = this.store.pipe(
      select(allFeatures),
      filter(f => f.customerOrdersView.enabled),
      map(f => f.customerOrdersView.enabled)
    );
  }

  ngOnDestroy() {
    this.componentActive = false;
  }

  onCustomerSelected(customer: Customer) {
    this.store.dispatch(selectCustomer({data: customer}));
    this.store.dispatch(clearSelectedOrder());
    this.store.dispatch(clearAllOrders());
    this.store.dispatch(loadCustomerOrders({data: customer.id}));
  }

  onBackToOrderList() {
    this.store.dispatch(clearSelectedOrder());
  }
}
