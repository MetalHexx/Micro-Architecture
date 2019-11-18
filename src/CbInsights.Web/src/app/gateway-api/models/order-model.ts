/* tslint:disable */
import { OrderItemModel } from './order-item-model';
export interface OrderModel {
  orderId?: number;
  orderDate?: string;
  items?: Array<OrderItemModel>;
}
