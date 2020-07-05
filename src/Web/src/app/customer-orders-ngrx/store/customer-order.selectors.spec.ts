import * as fromCustomerOrder from './customer-order.reducer';
import { selectCustomerOrderState } from './customer-order.selectors';

describe('CustomerOrder Selectors', () => {
  it('should select the feature state', () => {
    const result = selectCustomerOrderState({
      [fromCustomerOrder.customerOrderFeatureKey]: {}
    });

    expect(result).toEqual({});
  });
});
