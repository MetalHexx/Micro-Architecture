import { Action, props, createAction } from '@ngrx/store';
import { AppFeatures } from 'src/app/gateway-api/models';
// import { AppFeatures } from '../models/feature-settings';

export const getFeatures = createAction(
    '[Feature] Get Features from server');

export const getFeaturesSuccess = createAction(
    '[Feature] Successfully received features from server',
    props<{features: AppFeatures}>());

export const getFeaturesFailure = createAction(
    '[Feature] Failed to receive features from server');     
 