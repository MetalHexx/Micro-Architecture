import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderViewModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  @Input() orders: OrderViewModel[];
  @Output() orderSelected: EventEmitter<OrderViewModel> = new EventEmitter<OrderViewModel>();

  constructor() { }

  ngOnInit() {
  }

  onOrderSelected(order: OrderViewModel) {
    this.orderSelected.emit(order);
  }

}
