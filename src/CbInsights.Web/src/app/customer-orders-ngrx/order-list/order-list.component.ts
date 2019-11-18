import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  @Input() orders: OrderModel[];
  @Output() orderSelected: EventEmitter<OrderModel> = new EventEmitter<OrderModel>();

  constructor() { }

  ngOnInit() {
  }

  onOrderSelected(order: OrderModel) {
    this.orderSelected.emit(order);
  }

}
