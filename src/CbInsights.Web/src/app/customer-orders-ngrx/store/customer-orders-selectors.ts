import { createSelector, MemoizedSelector, createFeatureSelector } from "@ngrx/store";
import { CustomerOrdersState } from './customer-orders-state';
import { Customer, CustomerOrdersModel, OrderModel } from 'src/app/gateway-api/models';

const getCustomers = (state: CustomerOrdersState): Customer[] => state.customers;
const getSelectedCustomer = (state: CustomerOrdersState): Customer => state.selectedCustomer;
const getCustomersLoading = (state: CustomerOrdersState): boolean => state.customersLoading;
const getCustomersError = (state: CustomerOrdersState): boolean => state.customersError;

const getOrders = (state: CustomerOrdersState): CustomerOrdersModel => state.orders;
const getSelectedOrder = (state: CustomerOrdersState): Customer => state.selectedOrder;
const getOrdersLoading = (state: CustomerOrdersState): boolean => state.ordersLoading;
const getOrdersError = (state: CustomerOrdersState): boolean => state.ordersError;

export const selectCustomerOrdersState: MemoizedSelector<object, CustomerOrdersState> =
    createFeatureSelector<CustomerOrdersState>('customerOrders');

export const selectCustomers: MemoizedSelector<object, Customer[]> =
    createSelector(selectCustomerOrdersState, getCustomers);

export const selectSelectedCustomer: MemoizedSelector<object, Customer> =
    createSelector(selectCustomerOrdersState, getSelectedCustomer);

export const selectCustomersLoading: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrdersState, getCustomersLoading);

export const selectCustomersError: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrdersState, getCustomersError);

export const selectOrders: MemoizedSelector<object, CustomerOrdersModel> =
    createSelector(selectCustomerOrdersState, getOrders);

export const selectSelectedOrder: MemoizedSelector<object, OrderModel> =
    createSelector(selectCustomerOrdersState, getSelectedOrder);

export const selectOrdersLoading: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrdersState, getOrdersLoading);

export const selectOrdersError: MemoizedSelector<object, boolean> =
    createSelector(selectCustomerOrdersState, getOrdersError);