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
import { StoreModule } from '@ngrx/store';
import * as fromCustomerOrder from './store/customer-order.reducer';
import { EffectsModule } from '@ngrx/effects';
import { CustomerOrderEffects } from './store/customer-order.effects';
import { OrderContainerComponent } from './order-container/order-container.component';
import { SharedModule } from '../shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    CustomerOrdersViewComponent,
    CustomerSelectComponent,
    OrderListComponent,
    OrderDetailsComponent,
    OrderListItemComponent,
    OrderItemComponent,
    OrderContainerComponent],
  providers: [],
  imports: [
    CommonModule,
    GatewayApiModule,
    MaterialModule,
    SharedModule,
    BrowserAnimationsModule,
    StoreModule.forFeature(fromCustomerOrder.customerOrderFeatureKey, fromCustomerOrder.reducer),
    EffectsModule.forFeature([CustomerOrderEffects])
  ]
})
export class CustomerOrdersNgrxModule { }
