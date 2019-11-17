/* tslint:disable */
import { OrderItemModel } from './order-item-model';
export interface OrderModel {
  orderId?: number;
  items?: Array<OrderItemModel>;
}
