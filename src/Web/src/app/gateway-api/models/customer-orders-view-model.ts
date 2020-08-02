/* tslint:disable */
import { CustomerViewModel } from './customer-view-model';
import { OrderViewModel } from './order-view-model';
export interface CustomerOrdersViewModel {
  customer?: CustomerViewModel;
  orders?: Array<OrderViewModel>;
}
