import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../environments/environment';
import * as fromCustomerOrder from '../customer-orders-ngrx/store/customer-order.reducer';
import * as fromFeature from '../feature-management/store/feature.reducer';

export const appStateFeatureKey = 'appState';

export interface AppState {

  [fromCustomerOrder.customerOrderFeatureKey]: fromCustomerOrder.CustomerOrderState;
  [fromFeature.featureFeatureKey]: fromFeature.FeatureState;
}

export const reducers: ActionReducerMap<AppState> = {

  [fromCustomerOrder.customerOrderFeatureKey]: fromCustomerOrder.reducer,
  [fromFeature.featureFeatureKey]: fromFeature.reducer,
};


export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [] : [];
