/* tslint:disable */
import { OrderItem } from './order-item';
export interface Order {
  id?: number;
  customerId?: number;
  items?: Array<OrderItem>;
}
