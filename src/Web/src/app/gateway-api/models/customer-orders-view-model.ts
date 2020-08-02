/* tslint:disable */
import { CustomerModel } from './customer-model';
import { OrderModel } from './order-model';
export interface CustomerOrdersViewModel {
  customer?: CustomerModel;
  orders?: Array<OrderModel>;
}
