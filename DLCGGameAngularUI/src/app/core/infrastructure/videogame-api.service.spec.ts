import { TestBed } from '@angular/core/testing';

import { VideogameApiService } from './videogame-api.service';

describe('VideogameApiService', () => {
  let service: VideogameApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VideogameApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
