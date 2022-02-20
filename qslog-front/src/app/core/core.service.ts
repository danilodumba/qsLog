import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export abstract class CoreService {
  apiUrl: string;
  apiPath: string;
  
  constructor(protected client: HttpClient, path: string) {
    this.apiPath = '/' + path + '/';
    this.apiUrl = environment.api + this.apiPath;
  }

  protected post<T>(model: T): Observable<T> {
    return this.client.post<T>(this.apiUrl, model);
  }

  protected postWithUrl<T>(path: string, model: T): Observable<T> {
    const api = this.apiUrl + `${path}`;
    return this.client.post<T>(api, model);
  }

  protected putWithUrl<T>(path: string, model: T): Observable<T> {
    const api = this.apiUrl + `${path}`;
    return this.client.put<T>(api, model);
  }

  protected put<T>(model: T, id: string): Observable<T> {
    return this.client.put<T>(this.apiUrl + id, model);
  }

  protected get<T>(search: string): Observable<T> {
    return this.client.get<T>(this.apiUrl + search);
  }

  protected delete<T>(path: string): Observable<T> {
    return this.client.delete<T>(this.apiUrl + path);
  }
}
