import { Action, createReducer, on } from '@ngrx/store';
import * as FeatureActions from './feature.actions';
import { AppFeatures } from 'src/app/gateway-api/models';

export const featureFeatureKey = 'feature';

export interface FeatureState {
  features: AppFeatures,
  loading: boolean,
  loadingFailed: boolean

}

export const initialState: FeatureState = {
  features: {},
  loading: false,
  loadingFailed: false
};

const featureReducer = createReducer(
  initialState,

  on(FeatureActions.loadFeatures, state => ({
      ...state, 
      loading: true, 
      loadingFailed: false
  })),
  on(FeatureActions.loadFeaturesSuccess, (state, action) => ({
    ...state, 
    features: action.data, 
    loading: false, 
    loadingFailed: false
  })),
  on(FeatureActions.loadFeaturesFailure, (state) => ({
    ...state, 
    loading: false, 
    loadingFailed: true
  })),

);

export function reducer(state: FeatureState | undefined, action: Action) {
  return featureReducer(state, action); 
}
