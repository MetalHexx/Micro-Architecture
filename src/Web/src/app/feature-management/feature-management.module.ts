import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as fromFeature from './store/feature.reducer';
import { EffectsModule } from '@ngrx/effects';
import { FeatureEffects } from './store/feature.effects';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forFeature(fromFeature.featureFeatureKey, fromFeature.reducer),
    EffectsModule.forFeature([FeatureEffects])
  ]
})
export class FeatureManagementModule { }
