import { createFeatureSelector, createSelector, MemoizedSelector } from '@ngrx/store';
import * as fromFeature from './feature.reducer';
import { AppFeatures, Feature } from 'src/app/gateway-api/models';

const getFeatures = (state: fromFeature.FeatureState): AppFeatures => state.features;
const getViewCustomerFeature = (state: fromFeature.FeatureState): Feature => state.features.viewCustomers;
const getViewOrderDetailsFeature = (state: fromFeature.FeatureState): Feature => state.features.viewOrderDetails;

export const selectAppFeaturesState = createFeatureSelector<fromFeature.FeatureState>(
  fromFeature.featureFeatureKey
);

export const selectFeatures: MemoizedSelector<object, AppFeatures> =
  createSelector(selectAppFeaturesState, getFeatures); 

export const selectViewCustomersFeature: MemoizedSelector<object, Feature> =
  createSelector(selectAppFeaturesState, getViewCustomerFeature); 

export const selectViewOrderDetailsFeature: MemoizedSelector<object, Feature> =
  createSelector(selectAppFeaturesState, getViewOrderDetailsFeature); 
