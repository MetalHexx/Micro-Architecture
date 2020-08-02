import { createFeatureSelector, createSelector, MemoizedSelector } from '@ngrx/store';
import * as fromCustomerOrder from './customer-order.reducer';
import { CustomerOrderState } from './customer-order.reducer';
import { Customer } from 'src/app/gateway-api/models/customer';
import { CustomerOrdersViewModel, OrderModel } from 'src/app/gateway-api/models';

const getCustomers = (state: CustomerOrderState): Customer[] => state.customers;
const getSelectedCustomer = (state: CustomerOrderState): Customer => state.selectedCustomer;
const getCustomersLoading = (state: CustomerOrderState): boolean => state.customersLoading;
const getCustomersError = (state: CustomerOrderState): boolean => state.customersError;

const getOrders = (state: CustomerOrderState): CustomerOrdersViewModel => state.orders;
const getSelectedOrder = (state: CustomerOrderState): OrderModel => state.selectedOrder;
const getOrdersLoading = (state: CustomerOrderState): boolean => state.ordersLoading;
const getOrdersError = (state: CustomerOrderState): boolean => state.ordersError;

export const selectCustomerOrderState = createFeatureSelector<fromCustomerOrder.CustomerOrderState>(
  fromCustomerOrder.customerOrderFeatureKey
);

export const selectCustomers: MemoizedSelector<object, Customer[]> =
    createSelector(selectCustomerOrderState, getCustomers);

export const selectSelectedCustomer: MemoizedSelector<object, Customer> =
    createSelector(selectCustomerOrderState, getSelectedCustomer);

export const selectCustomersLoading: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrderState, getCustomersLoading);

export const selectCustomersError: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrderState, getCustomersError);

export const selectOrders: MemoizedSelector<object, CustomerOrdersViewModel> =
    createSelector(selectCustomerOrderState, getOrders);

export const selectSelectedOrder: MemoizedSelector<object, OrderModel> =
    createSelector(selectCustomerOrderState, getSelectedOrder);

export const selectOrdersLoading: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrderState, getOrdersLoading);

export const selectOrdersError: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrderState, getOrdersError);

