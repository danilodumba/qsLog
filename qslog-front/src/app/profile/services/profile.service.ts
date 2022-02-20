import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoreService } from 'src/app/core/core.service';
import { ProfileModel } from '../models/profile.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends CoreService {

  constructor(protected client: HttpClient) {
    super(client, 'profile');
  }

  savePassword(model: any): Observable<any>{
    return this.put<any>(model, 'change-password');
  }

  getProfile(): Observable<ProfileModel>{
    return this.get<ProfileModel>('');
  }
}
