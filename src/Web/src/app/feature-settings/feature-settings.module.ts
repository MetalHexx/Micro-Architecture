import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as fromFeatures from './store/feature-reducer'
import { EffectsModule } from '@ngrx/effects';
import { FeatureEffects } from './store/feature-effects';


@NgModule({
  declarations: [],
  providers: [FeatureEffects],
  imports: [
    EffectsModule.forFeature([FeatureEffects]),
    StoreModule.forFeature(fromFeatures.featureSettingsKey, fromFeatures.reducer),
    CommonModule
  ]
})
export class FeatureSettingsModule { }
