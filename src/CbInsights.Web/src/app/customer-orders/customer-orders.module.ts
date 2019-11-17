import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerOrdersViewComponent } from './customer-orders-view/customer-orders-view.component';
import { GatewayApiModule } from '../gateway-api/gateway-api.module';
import { MaterialModule } from '../material/material.module';




@NgModule({
  declarations: [CustomerOrdersViewComponent],
  imports: [
    CommonModule,
    GatewayApiModule,
    MaterialModule
  ]
})
export class CustomerOrdersModule { }
