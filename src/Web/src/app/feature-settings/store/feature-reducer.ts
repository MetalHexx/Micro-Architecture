import { createReducer, on, Action } from '@ngrx/store';
import { initialState } from './feature.state'
import * as FeatureActions from './feature-actions';
import { FeatureState } from './feature.state';

const featureReducer = createReducer(
    initialState,
    
    on(FeatureActions.getFeaturesSuccess, (state, payload) => ({ 
      ...payload.features })),
    
    on(FeatureActions.getFeaturesFailure, state => ({ 
        ...state, initialState })),
  
  );
  
  export function reducer(state: FeatureState | undefined, action: Action) {
    return featureReducer(state, action);
  }
 
  export const featureSettingsKey = 'features';  