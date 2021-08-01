import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoreService } from 'src/app/core/core.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService extends CoreService {

  constructor(protected client: HttpClient) {
    super(client, 'auth');
  }

  login(model: any): Observable<any>{
    return this.post<any>(model);
  }
}
