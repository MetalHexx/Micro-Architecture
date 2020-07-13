import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeatureManagementViewComponent } from './feature-management-view.component';

describe('FeatureManagementViewComponent', () => {
  let component: FeatureManagementViewComponent;
  let fixture: ComponentFixture<FeatureManagementViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FeatureManagementViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeatureManagementViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
