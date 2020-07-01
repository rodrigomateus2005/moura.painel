import { TestBed } from '@angular/core/testing';

import { PainelApiService } from './painel-api.service';

describe('PainelApiService', () => {
  let service: PainelApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PainelApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
