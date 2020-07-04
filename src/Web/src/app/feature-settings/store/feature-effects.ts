import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { FeaturesService } from '../../gateway-api/services/features.service';
import * as FeatureActions from './feature-actions';
import { AppFeatures } from 'src/app/gateway-api/models';
 
@Injectable()
export class FeatureEffects {

    getfeatures$ = createEffect(() => this.actions$.pipe(
        ofType(FeatureActions.getFeatures.type),
        mergeMap(() =>
            this.featuresService.GetFeatures() 
                .pipe(
                    map((response: AppFeatures) => {
                        return FeatureActions.getFeaturesSuccess({features: response}) 
                    }),
                    catchError((err) => {
                        return of(FeatureActions.getFeaturesFailure());    
                    })
                ))));
 
  constructor(
    private actions$: Actions,
    private featuresService: FeaturesService  
  ) {
    this.featuresService.rootUrl = "http://localhost:5000";    
  }    
}  