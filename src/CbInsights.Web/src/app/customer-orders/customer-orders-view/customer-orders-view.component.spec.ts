import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerOrdersViewComponent } from './customer-orders-view.component';

describe('CustomerOrdersViewComponent', () => {
  let component: CustomerOrdersViewComponent;
  let fixture: ComponentFixture<CustomerOrdersViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerOrdersViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerOrdersViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
