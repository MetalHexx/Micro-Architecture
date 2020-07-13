import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CustomerOrdersViewComponent } from './customer-orders/customer-orders-view/customer-orders-view.component';
import { CustomerOrdersViewComponent as CustomerOrdersViewNgrxComponent } from './customer-orders-ngrx/customer-orders-view/customer-orders-view.component';
import { FeatureManagementViewComponent } from './feature-management/feature-management-view/feature-management-view.component';


const routes: Routes = [
  { path: '', component: HomeComponent, data: { title: 'Home' } },
  { path: 'home', component: HomeComponent, data: { title: 'Home' } },
  { path: 'customer-orders', component: CustomerOrdersViewComponent, data: { title: 'Customers' } },
  { path: 'customer-orders-ngrx', component: CustomerOrdersViewNgrxComponent, data: { title: 'Customers' } },
  { path: 'feature-management', component: FeatureManagementViewComponent, data: { title: 'Feature Management' } },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
