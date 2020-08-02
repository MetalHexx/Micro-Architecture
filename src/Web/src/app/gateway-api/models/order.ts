/* tslint:disable */
import { OrderItem } from './order-item';
export interface Order {
  customerId?: number;
  id?: number;
  items?: Array<OrderItem>;
  orderDate?: string;
}
