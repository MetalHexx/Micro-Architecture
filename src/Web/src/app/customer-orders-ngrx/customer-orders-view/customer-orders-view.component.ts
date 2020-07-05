import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer, CustomerOrdersModel, OrderModel, Feature } from 'src/app/gateway-api/models';
import { Store, select } from '@ngrx/store';
import { filter, takeWhile, map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material';
import { AppState } from 'src/app/store';
import { loadCustomers, selectCustomer, clearOrder, selectOrder, loadCustomerOrders } from '../store/customer-order.actions';
import { selectCustomers, selectCustomersLoading, selectCustomersError, selectSelectedCustomer, selectOrders, selectOrdersLoading, selectOrdersError, selectSelectedOrder } from '../store/customer-order.selectors';
import { selectViewCustomersFeature, selectViewOrderDetailsFeature } from 'src/app/feature-management/store/feature.selectors';


@Component({
  selector: 'cbi-customer-orders-view',
  templateUrl: './customer-orders-view.component.html',
  styleUrls: ['./customer-orders-view.component.css']
})
export class CustomerOrdersViewComponent implements OnInit, OnDestroy {
  canViewCustomerFeature$: Observable<Feature>;
  canViewOrderDetails$: Observable<Feature>;
  customers$: Observable<Customer[]>;
  selectedCustomer$: Observable<Customer>;
  customersLoading$: Observable<boolean>;
  customersError$: Observable<boolean>;
  customerOrders$: Observable<CustomerOrdersModel>;
  selectedOrder$: Observable<OrderModel>; 
  ordersLoading$: Observable<boolean>;
  ordersError$: Observable<boolean>;
  componentActive: boolean = true;

  constructor(
    private store: Store<AppState>, private snackbar: MatSnackBar) { }

  ngOnInit() {
    this.store.dispatch(loadCustomers());


    this.canViewCustomerFeature$ = this.store.pipe(
      select(selectViewCustomersFeature),
      filter(s => s !== null),
      takeWhile(() => this.componentActive));

    this.canViewOrderDetails$ = this.store.pipe(
      select(selectViewOrderDetailsFeature),
      filter(s => s !== null),
      takeWhile(() => this.componentActive));

    this.customers$ = this.store.pipe(
      select(selectCustomers),
      filter(s => !this.isEmpty(s)),
      takeWhile(() => this.componentActive));

    this.customersLoading$ = this.store.pipe(
      select(selectCustomersLoading),
      takeWhile(() => this.componentActive));

    this.customersError$ = this.store.pipe(
      select(selectCustomersError),
      takeWhile(() => this.componentActive));

    this.selectedCustomer$ = this.store.pipe(
      select(selectSelectedCustomer),
      filter(s => !this.isEmpty(s)),
      takeWhile(() => this.componentActive));

    this.customerOrders$ = this.store.pipe(
      select(selectOrders),
      filter(s => !this.isEmpty(s)),
      takeWhile(() => this.componentActive));

    this.ordersLoading$ = this.store.pipe(
      select(selectOrdersLoading),
      takeWhile(() => this.componentActive));

    this.ordersError$ = this.store.pipe(
      select(selectOrdersError),
      takeWhile(() => this.componentActive));

    this.selectedOrder$ = this.store.pipe(
      select(selectSelectedOrder),
      takeWhile(() => this.componentActive));

    this.customersError$
      .pipe(
        filter(isError => {
          return isError !== false;
        }))
      .subscribe(error => {
        this.snackbar.open("Error fetching customers", null, { duration: 3000 });
      });


    this.ordersError$
      .pipe(
        filter(isError => isError !== false))
      .subscribe(error =>
        this.snackbar.open("Error fetching orders", null, { duration: 3000 }));
  }

  isEmpty(obj): boolean {
    return (obj && (Object.keys(obj).length === 0));
  }

  ngOnDestroy() {
    this.componentActive = false;
  }

  onCustomerSelected(customer: Customer) {
    this.store.dispatch(selectCustomer({data: customer}));
    this.store.dispatch(clearOrder());
    this.store.dispatch(loadCustomerOrders({data: customer.id}));
  }

  onOrderSelected(order: OrderModel) {
    this.store.dispatch(selectOrder({data: order}));
  }

  onBackToOrderList() {
    this.store.dispatch(clearOrder());
  }
}
