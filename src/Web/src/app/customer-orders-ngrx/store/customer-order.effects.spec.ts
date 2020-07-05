import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable } from 'rxjs';

import { CustomerOrderEffects } from './customer-order.effects';

describe('CustomerOrderEffects', () => {
  let actions$: Observable<any>;
  let effects: CustomerOrderEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        CustomerOrderEffects,
        provideMockActions(() => actions$)
      ]
    });

    effects = TestBed.get<CustomerOrderEffects>(CustomerOrderEffects);
  });

  it('should be created', () => {
    expect(effects).toBeTruthy();
  });
});
