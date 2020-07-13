import { createFeatureSelector, createSelector, MemoizedSelector } from '@ngrx/store';
import * as fromFeature from './feature.reducer';
import { AppFeatures, Feature } from 'src/app/gateway-api/models';

const getFeatures = (state: fromFeature.FeatureState): AppFeatures => state.features;
const getFeaturesLoading = (state: fromFeature.FeatureState): boolean => state.loading;
const getFeaturesLoadingFailed = (state: fromFeature.FeatureState): boolean => state.loadingFailed;

export const selectAppFeaturesState = createFeatureSelector<fromFeature.FeatureState>(
  fromFeature.featureFeatureKey
);

export const allFeatures: MemoizedSelector<object, AppFeatures> =
  createSelector(selectAppFeaturesState, getFeatures); 

export const featuresLoading: MemoizedSelector<object, boolean> =
  createSelector(selectAppFeaturesState, getFeaturesLoading); 

export const featuresLoadingFailed: MemoizedSelector<object, boolean> =
  createSelector(selectAppFeaturesState, getFeaturesLoadingFailed); 
