import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Survey, SurveyCreate, SurveySubmit } from '../interfaces/survey';
import { Question } from '../../questions/interfaces/question';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  private apiUrl = `${environment.apiUrl}/surveys`;
  private apiQuestionsUrl = `${environment.apiUrl}/questions`;
  constructor(private http: HttpClient) { }


  loadSurveys(filters?: { title?: string; status?: boolean, hasResponses?: boolean }) {
    let params = new HttpParams();

    if (filters?.title) {
      params = params.set('title', filters.title);
    }

    if (filters?.status !== undefined && filters?.status !== null) {
      params = params.set('status', filters.status);
    }

    if (filters?.hasResponses !== undefined && filters?.hasResponses !== null) {
      params = params.set('hasResponses', filters.hasResponses);
    }

    return this.http.get<Survey[]>(this.apiUrl, { params });
  }

  /*loadQuestions() {
    return this.http.get<Question[]>(this.apiQuestionsUrl);
  }*/

  loadQuestions(filters?: { title?: string; status?: boolean }) {
    let params = new HttpParams();

    if (filters?.title) {
      params = params.set('title', filters.title);
    }

    if (filters?.status !== undefined && filters?.status !== null) {
      params = params.set('status', filters.status);
    }

    return this.http.get<Question[]>(this.apiQuestionsUrl, { params });
  }

  getSurveyById(id: string) {
    return this.http.get<Survey>(`${this.apiUrl}/${id}`);
  }

  createSurvey(data: SurveyCreate) {
    return this.http.post(this.apiUrl, data);
  }

  updateSurvey(id: number, data: SurveyCreate) {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  submitSurvey(id: string, data: SurveySubmit) {
    return this.http.post(`${this.apiUrl}/${id}/submit`, data);
  }
}
