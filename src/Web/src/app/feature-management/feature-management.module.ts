import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as fromFeature from './store/feature.reducer';
import { EffectsModule } from '@ngrx/effects';
import { FeatureEffects } from './store/feature.effects';
import { FeatureManagementViewComponent } from './feature-management-view/feature-management-view.component';



@NgModule({
  exports: [FeatureManagementViewComponent],
  declarations: [FeatureManagementViewComponent],
  imports: [
    CommonModule,
    StoreModule.forFeature(fromFeature.featureFeatureKey, fromFeature.reducer),
    EffectsModule.forFeature([FeatureEffects])
  ]
})
export class FeatureManagementModule { }
