import { Routes } from '@angular/router';
import { ListaDeTarefasComponent } from './lista-de-tarefas/lista-de-tarefas.component';
import { VisualizarTarefaComponent } from './visualizar-tarefa/visualizar-tarefa.component';

export const routes: Routes = [
  { path: '', redirectTo: 'lista-de-tarefas', pathMatch: 'full' },
  { path: 'lista-de-tarefas', component: ListaDeTarefasComponent },
  { path: 'visualizar-tarefa/:idDaTarefa', component: VisualizarTarefaComponent },
];
