import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, concatMap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as FeatureActions from './feature.actions';
import { FeaturesService } from 'src/app/gateway-api/services';



@Injectable()
export class FeatureEffects {

  loadFeatures$ = createEffect(() => {
    return this.actions$.pipe( 

      ofType(FeatureActions.loadFeatures),
      concatMap(() =>
        this.featuresService.GetFeatures().pipe(
          map(data => FeatureActions.loadFeaturesSuccess({ data })),
          catchError(error => of(FeatureActions.loadFeaturesFailure({ error }))))
      )
    );
  }); 



  constructor(
    private actions$: Actions,
    private featuresService: FeaturesService  
  ) {
    this.featuresService.rootUrl = "http://localhost:5000";    
  }

}
