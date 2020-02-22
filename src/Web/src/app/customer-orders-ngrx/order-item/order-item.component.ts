import { Component, OnInit, Input } from '@angular/core';
import { OrderItemModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent implements OnInit {
  @Input() item: OrderItemModel;

  constructor() { }

  ngOnInit() {
  }

}
