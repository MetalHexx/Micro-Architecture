import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomersService, CustomerOrdersService } from 'src/app/gateway-api/services';
import { Customer } from 'src/app/gateway-api/models';


@Component({
  selector: 'cbi-customer-orders-view',
  templateUrl: './customer-orders-view.component.html',
  styleUrls: ['./customer-orders-view.component.css']
})
export class CustomerOrdersViewComponent implements OnInit {
  customers$: Observable<Customer[]>;
  selectedCustomer: Customer;

  constructor(
    private customersService: CustomersService,
    private customerOrdersService: CustomerOrdersService) { }

  ngOnInit() {
    this.customersService.rootUrl = "http://localhost:5000";
    this.customers$ = this.customersService.GetCustomers();


  }

  onCustomerSelected() {

  }

}
