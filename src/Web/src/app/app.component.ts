import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AppState } from './store';
import { loadFeatures } from './feature-management/store/feature.actions';
import { featuresLoadingFailed, allFeatures, featuresLoading } from './feature-management/store/feature.selectors';
import { Observable } from 'rxjs';
import { AppFeatures } from './gateway-api/models';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  featuresLoading$ : Observable<boolean>;
  features$: Observable<AppFeatures>
  featureLoadingFailed$: Observable<boolean>;

  constructor(private store: Store<AppState>) {
    

  }
  ngOnInit(): void {
    this.store.dispatch(loadFeatures());  

    this.features$ = this.store.pipe(
      select(allFeatures),
      filter(features => {
        return features !== null;
      })
    );

    this.featuresLoading$ = this.store.pipe(
      select(featuresLoading)
    );

    this.featureLoadingFailed$ = this.store.pipe(
      select(featuresLoadingFailed)
    );
  }
}
