import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoreService } from 'src/app/core/core.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectService extends CoreService {

  constructor(protected client: HttpClient) {
    super(client, 'project');
  }

  create(model: any): Observable<any>{
    console.log(model);
    return this.post<any>(model);
  }

  update(model: any, id: string): Observable<any>{
    return this.put<any>(model, id);
  }

  remove(id: string): Observable<any>{
    return this.delete<any>(id);
  }

  list(name: string): Observable<any>{
    return this.get<any>('?name=' + name);
  }

  getById(id: string): Observable<any>{
    return this.get<any>(id);
  }
}
