import { TestBed } from '@angular/core/testing';

import { AuthcredentialstoreService } from './authcredentialstore.service';

describe('AuthcredentialstoreService', () => {
  let service: AuthcredentialstoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthcredentialstoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
