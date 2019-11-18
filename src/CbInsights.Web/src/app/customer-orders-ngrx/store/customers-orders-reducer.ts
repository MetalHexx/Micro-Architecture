import { CustomerOrdersState, initialState } from './customer-orders-state';
import { CustomerOrdersActions, Actions } from './customer-orders-actions';

export function customerOrdersReducer(state: CustomerOrdersState = initialState, action: CustomerOrdersActions): CustomerOrdersState {
    switch (action.type) {
        case Actions.GetCustomers:
            return {
                ...state,
                customersLoading: true
            };

        case Actions.GetCustomersSuccess:
            return {
                ...state,
                customers: action.payload,
                customersLoading: false
            };

        case Actions.GetCustomersFailure:
            return {
                ...state,
                customers: initialState.customers,
                customersLoading: false,
                customersError: true
            };

        case Actions.GetCustomerOrders:
            return {
                ...state,
                ordersLoading: true
            };

        case Actions.GetCustomerOrdersSuccess:
            return {
                ...state,
                orders: action.payload,
                ordersLoading: false
            };

        case Actions.GetCustomerOrdersFailure:
            return {
                ...state,
                orders: initialState.orders,
                ordersLoading: false,
                ordersError: true
            };

        case Actions.SelectCustomer:
            return {
                ...state,
                selectedOrder: initialState.selectedOrder,
                selectedCustomer: action.payload
            };


        case Actions.SelectOrder:
            return {
                ...state,
                selectedOrder: action.payload
            };

        case Actions.ClearOrder:
            return {
                ...state,
                selectedOrder: initialState.selectedOrder
            }

        default:
            return state;
    }
}