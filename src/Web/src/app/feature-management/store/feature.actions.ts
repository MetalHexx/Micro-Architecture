import { createAction, props } from '@ngrx/store';

export const loadFeatures = createAction(
  '[Feature] Load Features'
);

export const loadFeaturesSuccess = createAction(
  '[Feature] Load Features Success',
  props<{ data: any }>()
);

export const loadFeaturesFailure = createAction(
  '[Feature] Load Features Failure',
  props<{ error: any }>()
);
