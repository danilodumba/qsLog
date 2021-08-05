import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoreService } from 'src/app/core/core.service';

@Injectable({
  providedIn: 'root'
})
export class LogService extends CoreService {

  constructor(protected client: HttpClient) {
    super(client, 'log');
  }

  list(initDate: string, endDate: string, description?: string, type?: any, projectID?: string): Observable<any>{
    var search = `?description=${description}&type=${type}&projectID=${projectID}`
    return this.get<any>(`${initDate}/${endDate}/${search}`);
  }

  getById(id: string): Observable<any>{
    return this.get<any>(id);
  }
}
