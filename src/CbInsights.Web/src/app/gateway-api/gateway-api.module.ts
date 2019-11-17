/* tslint:disable */
import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { GatewayApiConfiguration, GatewayApiConfigurationInterface } from './gateway-api-configuration';

import { CustomerOrdersService } from './services/customer-orders.service';
import { CustomersService } from './services/customers.service';
import { OrdersService } from './services/orders.service';
import { ProductsService } from './services/products.service';

/**
 * Provider for all GatewayApi services, plus GatewayApiConfiguration
 */
@NgModule({
  imports: [
    HttpClientModule
  ],
  exports: [
    HttpClientModule
  ],
  declarations: [],
  providers: [
    GatewayApiConfiguration,
    CustomerOrdersService,
    CustomersService,
    OrdersService,
    ProductsService
  ],
})
export class GatewayApiModule {
  static forRoot(customParams: GatewayApiConfigurationInterface): ModuleWithProviders {
    return {
      ngModule: GatewayApiModule,
      providers: [
        {
          provide: GatewayApiConfiguration,
          useValue: {rootUrl: customParams.rootUrl}
        }
      ]
    }
  }
}
