import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderViewModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-list-item',
  templateUrl: './order-list-item.component.html',
  styleUrls: ['./order-list-item.component.css']
})
export class OrderListItemComponent implements OnInit {
  @Input() order: OrderViewModel;
  @Output() orderSelected: EventEmitter<OrderViewModel> = new EventEmitter<OrderViewModel>();

  constructor() { }

  ngOnInit() {
  }

  onOrderSelected() {
    this.orderSelected.emit(this.order);
  }
}
