/* tslint:disable */
import { CustomerModel } from './customer-model';
import { OrderModel } from './order-model';
export interface CustomerOrdersModel {
  customer?: CustomerModel;
  orders?: Array<OrderModel>;
}
