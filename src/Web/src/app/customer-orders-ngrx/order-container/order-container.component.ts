import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { Observable } from 'rxjs';
import { CustomerOrdersViewModel } from 'src/app/gateway-api/models/customer-orders-view-model';
import { OrderModel } from 'src/app/gateway-api/models';
import { selectOrders, selectOrdersLoading, selectOrdersError, selectSelectedOrder } from '../store/customer-order.selectors';
import { filter, map } from 'rxjs/operators';
import { selectOrder, clearSelectedOrder } from '../store/customer-order.actions';

@Component({
  selector: 'cbi-order-container',
  templateUrl: './order-container.component.html',
  styleUrls: ['./order-container.component.css']
})
export class OrderContainerComponent implements OnInit {
  customerOrders$: Observable<CustomerOrdersViewModel>;
  selectedOrder$: Observable<OrderModel>;
  isOrderSelected$:  Observable<boolean>;
  loading$: Observable<boolean>;
  error$: Observable<boolean>;

  constructor(private store: Store<AppState>) { }

  ngOnInit() {
    this.customerOrders$ = this.store.pipe(
      select(selectOrders),
      filter(o => o !== null));

    this.loading$ = this.store.pipe(
      select(selectOrdersLoading));

    this.error$ = this.store.pipe(
      select(selectOrdersError));

    this.selectedOrder$ = this.store.pipe(
      select(selectSelectedOrder));

    this.isOrderSelected$ = this.selectedOrder$.pipe(
      map(selectedOrder => selectedOrder !== null)
    )
  }

  onOrderSelected(order: OrderModel) {
    this.store.dispatch(selectOrder({data: order}));
  }

  onBackToOrderList() {
    this.store.dispatch(clearSelectedOrder());
  }
}
