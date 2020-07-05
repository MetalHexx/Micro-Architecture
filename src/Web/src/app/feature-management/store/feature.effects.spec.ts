import { TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable } from 'rxjs';

import { FeatureEffects } from './feature.effects';

describe('FeatureEffects', () => {
  let actions$: Observable<any>;
  let effects: FeatureEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        FeatureEffects,
        provideMockActions(() => actions$)
      ]
    });

    effects = TestBed.get<FeatureEffects>(FeatureEffects);
  });

  it('should be created', () => {
    expect(effects).toBeTruthy();
  });
});
