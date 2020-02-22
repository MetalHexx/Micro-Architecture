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
    customers: [],
    selectedCustomer: {},
    customersLoading: false,
    customersError: false,
    orders: {},
    selectedOrder: {},
    ordersLoading: false,
    ordersError: false
};