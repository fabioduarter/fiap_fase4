import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TarefasService } from '../tarefas.service';
import { NgFor } from '@angular/common';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-visualizar-tarefa',
  standalone: true,
  imports: [NgFor, ReactiveFormsModule],
  templateUrl: './visualizar-tarefa.component.html',
  styleUrl: './visualizar-tarefa.component.css'
})
export class VisualizarTarefaComponent {
  
  opcoesImportancia = [
    {indice: 0, valor: "Critica", descricao: "Crítica"}, 
    {indice: 1, valor: "Alta", descricao: "Alta"}, 
    {indice: 2, valor: "Media", descricao: "Media"}, 
    {indice: 3, valor: "Baixa", descricao: "Baixa"}
  ];
  statusTela = "";
  idDaTarefa: string = '';
  tarefa  = this.formBuilder.group({
                                      id: [],
                                      nome: ['', Validators.required],
                                      descricao: ['', Validators.required],
                                      importancia: ['', Validators.required],
                                      prazo: ['', Validators.required],
                                      dataDoCadastro: [],
                                      dataDaConclusao: []
                                    });

  constructor(private route: ActivatedRoute, private router : Router
            ,private tarefasService: TarefasService, private formBuilder: FormBuilder,) { }

  ngOnInit(): void {
    
    this.statusTela = this.router.url.split('/')[1];
    this.idDaTarefa = this.route.snapshot.paramMap.get('idDaTarefa') ?? '';

    if (this.idDaTarefa !== ''){
      this.tarefasService.obterUmaTarefa(parseInt(this.idDaTarefa)).subscribe(
        tarefa => {
          
          this.tarefa.patchValue(tarefa);
        
        
      }, error => {
        // Se ocorrer um erro que não seja 404
        if (error.status === 404){
          //redireciona para pagina anterior ( de listagem )
          this.router.navigate(['/lista-de-tarefas']);
          //url('lista-de-tarefas');
        }
        console.error('Ocorreu um erro:', error.status);
      });
    }
    
  }

  onSubmit() : void {

  }
  salvar(tarefa : any): void {
    //realiza a validação do formulário
    this.tarefasService.salvarNovaTarefa(tarefa).subscribe(response => {
      console.log('response');
      console.log(response);
    });
  }
}
