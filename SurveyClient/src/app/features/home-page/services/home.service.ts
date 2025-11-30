import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Statics } from '../interfaces/home';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private apiUrl = `${environment.apiUrl}/Auth/statics`;

  constructor(private http: HttpClient) { }

  loadStatics() {
    return this.http.get<Statics>(this.apiUrl);
  }
}
