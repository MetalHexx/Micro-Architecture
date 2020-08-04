/* tslint:disable */
import { OrderItemViewModel } from './order-item-view-model';
export interface OrderViewModel {
  orderId?: number;
  orderDate?: string;
  items?: Array<OrderItemViewModel>;
}
