import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-list-item',
  templateUrl: './order-list-item.component.html',
  styleUrls: ['./order-list-item.component.css']
})
export class OrderListItemComponent implements OnInit {
  @Input() order: OrderModel;
  @Output() orderSelected: EventEmitter<OrderModel> = new EventEmitter<OrderModel>();

  constructor() { }

  ngOnInit() {
  }

  onOrderSelected() {
    this.orderSelected.emit(this.order);
  }
}
