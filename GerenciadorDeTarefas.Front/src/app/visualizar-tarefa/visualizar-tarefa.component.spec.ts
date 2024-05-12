import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisualizarTarefaComponent } from './visualizar-tarefa.component';

describe('VisualizarTarefaComponent', () => {
  let component: VisualizarTarefaComponent;
  let fixture: ComponentFixture<VisualizarTarefaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VisualizarTarefaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VisualizarTarefaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
