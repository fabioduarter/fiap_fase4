import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from 'zone.js/lib/zone-impl';

@Injectable({
  providedIn: 'root'
})
export class TarefasService {

  private baseUrl = 'http://localhost:8000/api/tasks'; // URL da sua API de tarefas

  constructor(private http: HttpClient) { }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.baseUrl);
  }

}
