import { BrowserModule } from '@angular/platform-browser';
import { NgModule, InjectionToken } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { MaterialModule } from './material/material.module';
import { CustomerOrdersModule } from './customer-orders/customer-orders.module';
import { CustomerOrdersNgrxModule } from './customer-orders-ngrx/customer-orders-ngrx.module';
import { GatewayApiModule } from './gateway-api/gateway-api.module';
import { HomeComponent } from './home/home.component'
import { StoreModule } from '@ngrx/store';
import { reducers, AppState, metaReducers } from './store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { AppEffects } from './store/app.effects';
import { FeatureManagementModule } from './feature-management/feature-management.module';
import { SharedModule } from './shared/shared.module';
import { OverlayModule } from '@angular/cdk/overlay';
// import { reducers, metaReducers } from './store/reducers';

export const REDUCERS = new InjectionToken<AppState>('Root Reducer');

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
    MaterialModule,
    CustomerOrdersModule,
    CustomerOrdersNgrxModule, 
    GatewayApiModule,
    FeatureManagementModule,
    SharedModule,
    OverlayModule,
    StoreModule.forRoot(REDUCERS, {
      metaReducers, 
      runtimeChecks: {
        strictStateImmutability: true,
        strictActionImmutability: true,
      }
    }),
    StoreDevtoolsModule.instrument(),
    EffectsModule.forRoot([AppEffects]),
  ],
  providers: [{provide: REDUCERS, useValue: reducers}],
  bootstrap: [AppComponent]
  
})
export class AppModule { }
