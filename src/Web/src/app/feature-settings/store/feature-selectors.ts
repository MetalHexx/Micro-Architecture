
import { Customer, CustomerOrdersModel, OrderModel, AppFeatures, Feature } from 'src/app/gateway-api/models';
import { FeatureState } from './feature.state';
import { MemoizedSelector, createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromFeatures from './feature-reducer'

const getFeatures = (state: any): AppFeatures => state;
const getViewCustomerFeature = (state: FeatureState): Feature => state.viewCustomers;


export const selectFeaturesState: MemoizedSelector<object, FeatureState> =
    createFeatureSelector<FeatureState>(fromFeatures.featureSettingsKey);

export const selectFeatures: MemoizedSelector<object, AppFeatures> =
    createSelector(selectFeaturesState, getFeatures); 

export const selectViewCustomersFeature: MemoizedSelector<object, Feature> =
createSelector(selectFeaturesState, getViewCustomerFeature); 

