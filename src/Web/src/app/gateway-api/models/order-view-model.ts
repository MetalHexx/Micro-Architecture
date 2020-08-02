/* tslint:disable */
import { OrderItemViewModel } from './order-item-view-model';
export interface OrderViewModel {
  items?: Array<OrderItemViewModel>;
  orderDate?: string;
  orderId?: number;
}
