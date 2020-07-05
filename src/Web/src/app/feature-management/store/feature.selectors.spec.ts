import * as fromFeature from './feature.reducer';
import { selectFeatureState } from './feature.selectors';

describe('Feature Selectors', () => {
  it('should select the feature state', () => {
    const result = selectFeatureState({
      [fromFeature.featureFeatureKey]: {}
    });

    expect(result).toEqual({});
  });
});
