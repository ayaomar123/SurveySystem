import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Statics, SurveyAnalytics } from '../interfaces/home';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private apiUrl = `${environment.apiUrl}/Auth/statics`;
  private apiAnalyticsUrl = `${environment.apiUrl}/Surveys/`;

  constructor(private http: HttpClient) { }

  loadStatics() {
    return this.http.get<Statics>(this.apiUrl);
  }

  getAnalytics(id: string) {
    return this.http.get<SurveyAnalytics>(this.apiAnalyticsUrl + id + '/analytics');
  }
}
