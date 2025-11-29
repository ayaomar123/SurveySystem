import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Survey, SurveyCreate } from '../interfaces/survey';
import { Question } from '../../questions/interfaces/question';

@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  private apiUrl = `${environment.apiUrl}/surveys`;
  private apiQuestionsUrl = `${environment.apiUrl}/questions`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadSurveys();
  }

  loadSurveys() {
    return this.http.get<Survey[]>(this.apiUrl);
  }

  loadQuestions() {
    return this.http.get<Question[]>(this.apiQuestionsUrl);
  }
  createSurvey(data: SurveyCreate) {
    return this.http.post(this.apiUrl, data);
  }

  updateSurvey(id: number, data: SurveyCreate) {
    console.log("Updating question with ID:", data.id);
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }
}
