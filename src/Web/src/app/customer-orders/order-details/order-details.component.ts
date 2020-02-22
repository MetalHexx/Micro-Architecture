import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { OrderModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-details',
  templateUrl: './order-details.component.html',
  styleUrls: ['./order-details.component.css']
})
export class OrderDetailsComponent implements OnInit {
  @Input() order: OrderModel;
  @Output() orderClosed: EventEmitter<any> = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }

  onOrderClosed() {
    this.orderClosed.emit();
  }
}
