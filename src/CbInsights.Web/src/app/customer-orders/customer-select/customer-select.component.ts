import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Customer } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-customer-select',
  templateUrl: './customer-select.component.html',
  styleUrls: ['./customer-select.component.css']
})
export class CustomerSelectComponent implements OnInit {
  @Input() customers: Customer[];
  @Output() customerSelected: EventEmitter<Customer> = new EventEmitter<Customer>();

  constructor() { }

  ngOnInit() {
  }

  onCustomerSelected(customerSelection: any) {
    this.customerSelected.emit(customerSelection.value);
  }
}
