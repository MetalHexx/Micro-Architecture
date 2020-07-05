import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from './store';
import { loadFeatures } from './feature-management/store/feature.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private store: Store<AppState>) {
    

  }
  ngOnInit(): void {
    this.store.dispatch(loadFeatures());  
    
  }
  
}
