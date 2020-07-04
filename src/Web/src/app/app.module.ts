import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { MaterialModule } from './material/material.module';
import { CustomerOrdersModule } from './customer-orders/customer-orders.module';
import { CustomerOrdersNgrxModule } from './customer-orders-ngrx/customer-orders-ngrx.module';
import { GatewayApiModule } from './gateway-api/gateway-api.module';
import { HomeComponent } from './home/home.component'
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { FeatureSettingsModule } from './feature-settings/feature-settings.module';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    MaterialModule,
    CustomerOrdersModule,
    CustomerOrdersNgrxModule,
    GatewayApiModule,
    FeatureSettingsModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
