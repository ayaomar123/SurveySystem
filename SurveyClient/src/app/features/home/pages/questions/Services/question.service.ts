import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../../../../../environments/environment.development';
import { Question } from '../interfaces/question';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QuestionService implements OnInit {
  private apiUrl = `${environment.apiUrl}/questions`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadQuestions();
  }

  loadQuestions() {
    return this.http.get<Question[]>(this.apiUrl);
  }

  createQuestion(Question: Question) {
    return this.http.post(this.apiUrl, Question);
  }

  updateQuestion(Question: Question) {
    return this.http.put(`${this.apiUrl}/${Question.id}/edit`, Question);
  }

  updateStatus(id: number) {
    return this.http.patch(`${this.apiUrl}/${id}/status`, id);
  }
}
