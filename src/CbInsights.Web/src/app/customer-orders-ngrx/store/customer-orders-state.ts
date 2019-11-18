import { Customer, CustomerOrdersModel, OrderModel } from 'src/app/gateway-api/models';

//Feature State Definition
export interface CustomerOrdersState {
    customers: Customer[],
    selectedCustomer: Customer,
    customersLoading: boolean,
    customersError: boolean
    orders: CustomerOrdersModel,
    selectedOrder: OrderModel,
    ordersLoading: boolean,
    ordersError: boolean

}

//State Definition Initializer
export const initialState: CustomerOrdersState = {
    customers: null,
    selectedCustomer: null,
    customersLoading: false,
    customersError: false,
    orders: null,
    selectedOrder: null,
    ordersLoading: false,
    ordersError: false
};