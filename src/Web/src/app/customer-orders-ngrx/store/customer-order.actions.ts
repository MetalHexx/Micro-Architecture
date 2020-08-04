import { createAction, props } from '@ngrx/store';
import { CustomerViewModel } from 'src/app/gateway-api/models/customer-view-model';
import { CustomerOrdersViewModel, OrderViewModel } from 'src/app/gateway-api/models';

export const loadCustomers = createAction(
  '[CustomerOrders] Get Customers');

export const loadCustomersSuccess = createAction(
  '[CustomerOrders] Load Customers Success',
  props<{ data: CustomerViewModel[] }>());

export const loadCustomersFailure = createAction(
  '[CustomerOrders] Load Customers Failure');

export const loadCustomerOrders = createAction(
  '[CustomerOrders] Load Customer Orders',
  props<{ data: number }>());

export const loadCustomerOrdersSuccess = createAction(
  '[CustomerOrders] Load Customer Orders Success',
  props<{ data: CustomerOrdersViewModel }>());

export const loadCustomersOrdersFailure = createAction(
  '[CustomerOrders] Load Customer Orders Failure');

export const selectCustomer = createAction(
  '[CustomerOrders] Customer Selected',
  props<{ data: CustomerViewModel }>());

export const selectOrder = createAction(
  '[CustomerOrders] Order Selected',
  props<{ data: OrderViewModel }>());

export const clearSelectedOrder = createAction(
  '[CustomerOrders] Selected Order Cleared');


export const clearAllOrders = createAction(
  '[CustomerOrders] All Orders Cleared');


