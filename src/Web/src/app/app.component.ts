import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { FeatureState } from './feature-settings/store/feature.state';
import { getFeatures } from './feature-settings/store/feature-actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private featureStore: Store<FeatureState>) {
    

  }
  ngOnInit(): void {
    this.featureStore.dispatch(getFeatures());  
    
  }
  
}
