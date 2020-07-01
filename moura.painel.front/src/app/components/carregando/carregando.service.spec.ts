import { TestBed } from '@angular/core/testing';

import { CarregandoService } from './carregando.service';

describe('CarregandoService', () => {
  let service: CarregandoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarregandoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
