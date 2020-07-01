import { TestBed } from '@angular/core/testing';

import { RotaLogadaGuard } from './rota-logada.guard';

describe('RotaLogadaGuard', () => {
  let guard: RotaLogadaGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(RotaLogadaGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
