import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer, CustomerOrdersModel, OrderModel } from 'src/app/gateway-api/models';
import { Store, select } from '@ngrx/store';
import { CustomerOrdersState } from '../store/customer-orders-state';
import { GetCustomers, SelectCustomer, ClearOrder, GetCustomerOrders, SelectOrder } from '../store/customer-orders-actions';
import { selectCustomers, selectOrders, selectCustomersLoading, selectOrdersLoading, selectOrdersError, selectSelectedCustomer, selectSelectedOrder } from '../store/customer-orders-selectors';
import { filter, takeWhile } from 'rxjs/operators';


@Component({
  selector: 'cbi-customer-orders-view',
  templateUrl: './customer-orders-view.component.html',
  styleUrls: ['./customer-orders-view.component.css']
})
export class CustomerOrdersViewComponent implements OnInit, OnDestroy {
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
    private store: Store<CustomerOrdersState>) { }

  ngOnInit() {
    this.store.dispatch(new GetCustomers());

    this.customers$ = this.store.pipe(
      select(selectCustomers),
      filter(s => s !== null),
      takeWhile(() => this.componentActive));

    this.customersLoading$ = this.store.pipe(
      select(selectCustomersLoading),
      takeWhile(() => this.componentActive));

    this.customersError$ = this.store.pipe(
      select(selectCustomersLoading),
      takeWhile(() => this.componentActive));

    this.selectedCustomer$ = this.store.pipe(
      select(selectSelectedCustomer),
      filter(s => s !== null),
      takeWhile(() => this.componentActive));

    this.customerOrders$ = this.store.pipe(
      select(selectOrders),
      filter(s => s !== null),
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
  }

  ngOnDestroy() {
    this.componentActive = false;
  }

  onCustomerSelected(customer: Customer) {
    this.store.dispatch(new SelectCustomer(customer));
    this.store.dispatch(new ClearOrder());
    this.store.dispatch(new GetCustomerOrders(customer.id));
  }

  onOrderSelected(order: OrderModel) {
    this.store.dispatch(new SelectOrder(order));
  }

  onBackToOrderList() {
    this.store.dispatch(new ClearOrder());
  }
}
