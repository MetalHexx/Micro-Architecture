import { Component, OnInit, Input } from '@angular/core';
import { OrderItemViewModel } from 'src/app/gateway-api/models';

@Component({
  selector: 'cbi-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent implements OnInit {
  @Input() item: OrderItemViewModel;

  constructor() { }

  ngOnInit() {
  }

}
