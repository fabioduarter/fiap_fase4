import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TarefasService {

  private baseUrl = 'https://localhost:44388/api'; // URL da sua API de tarefas

  constructor(private http: HttpClient) { }

  obterTarefas(): Observable<any> 
  {
    return this.http.get<any>(`${this.baseUrl}/tarefas`,this.getHttpOptions());
  }

  obterUmaTarefa(idDaTarefa : number): Observable<any> 
  {
    return this.http.get<any>(`${this.baseUrl}/tarefas/${idDaTarefa}`);
  }

  salvarNovaTarefa(tarefa : any) : Observable<any> 
  {
    return this.http.post(`${this.baseUrl}/tarefas`,tarefa);
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      })
    };
  }

}
