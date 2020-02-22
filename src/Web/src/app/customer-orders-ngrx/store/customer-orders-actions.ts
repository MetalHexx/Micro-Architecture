import { Action } from "@ngrx/store";
import { Customer, OrderModel, CustomerOrdersModel } from 'src/app/gateway-api/models';

//Action Type Enumeration
export enum Actions {
    GetCustomers = '[CustomerOrders] Get Customers',
    GetCustomersSuccess = '[CustomerOrders] Get Customers Success',
    GetCustomersFailure = '[CustomerOrders] Get Customers Failure',
    GetCustomerOrders = '[CustomerOrders] Get Customer Orders',
    GetCustomerOrdersSuccess = '[CustomerOrders] Get Customer Orders Success',
    GetCustomerOrdersFailure = '[CustomerOrders] Get Customer Orders Failure',
    SelectCustomer = '[CustomerOrders] Customer Selected',
    SelectOrder = '[CustomerOrders] Order Selected',
    ClearOrder = '[CustomerOrders] Order Cleared'
}

//Action Creators -- Enables the dispatching of actions in a strongly typed fashion
export class GetCustomers implements Action {
    readonly type = Actions.GetCustomers
}

export class GetCustomersSuccess implements Action {
    readonly type = Actions.GetCustomersSuccess
    constructor(public payload: Customer[]) { }
}

export class GetCustomersFailure implements Action {
    readonly type = Actions.GetCustomersFailure
}

export class GetCustomerOrders implements Action {
    readonly type = Actions.GetCustomerOrders
    constructor(public payload: number) { }
}

export class GetCustomerOrdersSuccess implements Action {
    readonly type = Actions.GetCustomerOrdersSuccess
    constructor(public payload: CustomerOrdersModel) { }
}

export class GetCustomerOrdersFailure implements Action {
    readonly type = Actions.GetCustomerOrdersFailure
}

export class SelectCustomer implements Action {
    readonly type = Actions.SelectCustomer
    constructor(public payload: Customer) { }
}

export class SelectOrder implements Action {
    readonly type = Actions.SelectOrder
    constructor(public payload: OrderModel) { }
}

export class ClearOrder implements Action {
    readonly type = Actions.ClearOrder
}

//Union Action Types into 1 Type to simplify usage in the reducer
export type CustomerOrdersActions =
    GetCustomers |
    GetCustomersSuccess |
    GetCustomersFailure |
    GetCustomerOrders |
    GetCustomerOrdersSuccess |
    GetCustomerOrdersFailure |
    SelectCustomer |
    SelectOrder |
    ClearOrder
