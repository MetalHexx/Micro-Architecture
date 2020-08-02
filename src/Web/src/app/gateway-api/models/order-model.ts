/* tslint:disable */
import { OrderItemModel } from './order-item-model';
export interface OrderModel {
  items?: Array<OrderItemModel>;
  orderDate?: string;
  orderId?: number;
}
