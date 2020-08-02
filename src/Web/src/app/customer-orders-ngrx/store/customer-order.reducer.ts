import { Action, createReducer, on } from '@ngrx/store';
import * as CustomerOrderActions from './customer-order.actions';
import { Customer } from 'src/app/gateway-api/models/customer';
import { CustomerOrdersViewModel, OrderViewModel } from 'src/app/gateway-api/models';

export const customerOrderFeatureKey = 'customerOrder';

export interface CustomerOrderState {
  customers: Customer[],
  selectedCustomer: Customer,
  customersLoading: boolean,
  customersError: boolean
  orders: CustomerOrdersViewModel,
  selectedOrder: OrderViewModel,
  ordersLoading: boolean,
  ordersError: boolean

}

export const initialState: CustomerOrderState = {
  customers: null,
  selectedCustomer: null,
  customersLoading: false,
  customersError: false,
  orders: null,
  selectedOrder: null,
  ordersLoading: false,
  ordersError: false
};

const customerOrderReducer = createReducer(
  initialState,

  on(CustomerOrderActions.loadCustomers, state => ({
    ...state,
    customersLoading: true,
    customersError: false
  })),
  on(CustomerOrderActions.loadCustomersSuccess, (state, action) => ({
    ...state,
    customers: action.data,
    customersLoading: false,
    customersError: false
  })),
  on(CustomerOrderActions.loadCustomersFailure, (state) => ({
    ...state,
    customersLoading: false,
    customersError: true
  })),

  on(CustomerOrderActions.loadCustomerOrders, state => ({
    ...state,
    ordersLoading: true,
    orderssError: false
  })),

  on(CustomerOrderActions.loadCustomerOrdersSuccess, (state, action) => ({
    ...state,
    orders: action.data,
    ordersLoading: false,
    ordersError: false
  })),

  on(CustomerOrderActions.loadCustomersOrdersFailure, (state) => ({
    ...state,
    ordersLoading: false,
    ordersError: true
  })),

  on(CustomerOrderActions.clearAllOrders, (state) => ({
    ...state,
    selectedOrder: initialState.selectedOrder,
    orders: initialState.orders,
    ordersError: false,
    ordersLoading: false
  })),

  on(CustomerOrderActions.selectCustomer, (state, action) => ({
    ...state,
    selectedOrder: initialState.selectedOrder,
    selectedCustomer: action.data,
  })),

  on(CustomerOrderActions.selectOrder, (state, action) => ({
    ...state,
    selectedOrder: action.data
  })),

  on(CustomerOrderActions.clearSelectedOrder, (state, action) => ({
    ...state,
    selectedOrder: initialState.selectedOrder
  }))
);

export function reducer(state: CustomerOrderState | undefined, action: Action) {
  return customerOrderReducer(state, action);
}
