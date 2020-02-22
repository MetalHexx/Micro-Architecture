import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerOrdersViewComponent } from './customer-orders-view/customer-orders-view.component';
import { GatewayApiModule } from '../gateway-api/gateway-api.module';
import { MaterialModule } from '../material/material.module';
import { CustomerSelectComponent } from './customer-select/customer-select.component';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrderListItemComponent } from './order-list-item/order-list-item.component';
import { OrderItemComponent } from './order-item/order-item.component';




@NgModule({
  declarations: [
    CustomerOrdersViewComponent,
    CustomerSelectComponent,
    OrderListComponent,
    OrderDetailsComponent,
    OrderListItemComponent,
    OrderItemComponent],
  imports: [
    CommonModule,
    GatewayApiModule,
    MaterialModule
  ]
})
export class CustomerOrdersModule { }
