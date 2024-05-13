import { Component } from '@angular/core';
import { Task } from 'zone.js/lib/zone-impl';
import { TarefasService } from '../tarefas.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-lista-de-tarefas',
  standalone: true,
  imports: [NgFor],
  templateUrl: './lista-de-tarefas.component.html',
  styleUrl: './lista-de-tarefas.component.css'
})
export class ListaDeTarefasComponent {

  tarefas: any[] = [];

  constructor(private tarefasService: TarefasService) { }

  ngOnInit(): void {
    this.obterTarefas();
  }

  obterTarefas(): void {
    this.tarefasService.obterTarefas()
      .subscribe(tarefas => {
        console.log(tarefas);
        return this.tarefas = tarefas;
      });
  }
}
