import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TarefasService {

  private baseUrl = 'http://localhost:5262/api/tarefas'; // URL da sua API de tarefas

  constructor(private http: HttpClient) { }

  obterTarefas(): Observable<any> {

    return this.http.get<any>(this.baseUrl,this.getHttpOptions());
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      })
    };
  }

}
