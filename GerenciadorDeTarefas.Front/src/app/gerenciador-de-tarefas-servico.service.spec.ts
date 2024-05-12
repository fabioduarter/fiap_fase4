import { TestBed } from '@angular/core/testing';

import { GerenciadorDeTarefasServicoService } from './gerenciador-de-tarefas-servico.service';

describe('GerenciadorDeTarefasServicoService', () => {
  let service: GerenciadorDeTarefasServicoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GerenciadorDeTarefasServicoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
