import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Customer, Feature } from 'src/app/gateway-api/models';
import { Observable } from 'rxjs/internal/Observable';
import { Store, select } from '@ngrx/store';
import { AppState } from 'src/app/store';
import { selectCustomers, selectCustomersLoading, selectCustomersError } from '../store/customer-order.selectors';
import { filter } from 'rxjs/operators';
import { loadCustomers } from '../store/customer-order.actions';

@Component({
  selector: 'cbi-customer-select',
  templateUrl: './customer-select.component.html',
  styleUrls: ['./customer-select.component.css']
})
export class CustomerSelectComponent implements OnInit {
  customers$: Observable<Customer[]>;
  loading$: Observable<boolean>;
  error$: Observable<boolean>;
  @Output() customerSelected: EventEmitter<Customer> = new EventEmitter<Customer>();

  constructor( private store: Store<AppState>) { }

  ngOnInit() {
    this.store.dispatch(loadCustomers());
    
    this.customers$ = this.store.pipe(
      select(selectCustomers),
      filter(c => c !== null));

    this.loading$ = this.store.pipe(
      select(selectCustomersLoading));

    this.error$ = this.store.pipe(
      select(selectCustomersError),
      filter(error => error));
  }

  onCustomerSelected(customerSelection: any) {
    this.customerSelected.emit(customerSelection.value);
  }
}
