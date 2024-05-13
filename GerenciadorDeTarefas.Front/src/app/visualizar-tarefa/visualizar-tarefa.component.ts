import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-visualizar-tarefa',
  standalone: true,
  imports: [],
  templateUrl: './visualizar-tarefa.component.html',
  styleUrl: './visualizar-tarefa.component.css'
})
export class VisualizarTarefaComponent {
  idDaTarefa: string = '';

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.idDaTarefa = this.route.snapshot.paramMap.get('idDaTarefa') ?? '';
  }
}
