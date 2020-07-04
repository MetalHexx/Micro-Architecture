import { AppFeatures } from 'src/app/gateway-api/models';

export interface FeatureState extends AppFeatures {}

export const initialState: FeatureState = {};