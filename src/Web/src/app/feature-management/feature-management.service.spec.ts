import { TestBed } from '@angular/core/testing';

import { FeatureManagementService } from './feature-management.service';

describe('FeatureManagementService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FeatureManagementService = TestBed.get(FeatureManagementService);
    expect(service).toBeTruthy();
  });
});
