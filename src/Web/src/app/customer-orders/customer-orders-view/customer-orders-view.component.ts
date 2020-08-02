import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomersService, CustomerOrdersService } from 'src/app/gateway-api/services';
import { Customer, Order, CustomerOrdersViewModel, OrderViewModel } from 'src/app/gateway-api/models';


@Component({
  selector: 'cbi-customer-orders-view',
  templateUrl: './customer-orders-view.component.html',
  styleUrls: ['./customer-orders-view.component.css']
})
export class CustomerOrdersViewComponent implements OnInit {
  customers$: Observable<Customer[]>;
  customerOrders$: Observable<CustomerOrdersViewModel>;
  selectedOrder: Order = null;
  selectedCustomer: Customer = null;

  constructor(
    private customersService: CustomersService,
    private customerOrdersService: CustomerOrdersService) { }

  ngOnInit() {
    this.customersService.rootUrl = "http://localhost:5000";
    this.customerOrdersService.rootUrl = "http://localhost:5000";
    this.customers$ = this.customersService.GetCustomers();
  }

  onCustomerSelected(customer: Customer) {
    this.selectedCustomer = customer;
    this.selectedOrder = null;
    this.customerOrders$ = this.customerOrdersService.GetCustomerOrders({ customerId: customer.id });
  }

  onOrderSelected(order: OrderViewModel) {
    this.selectedOrder = order;
  }

  onBackToOrderList() {
    this.selectedOrder = null;
  }
}
