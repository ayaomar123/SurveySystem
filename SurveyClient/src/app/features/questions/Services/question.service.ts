import { Injectable, OnInit } from '@angular/core';
import { Question } from '../interfaces/question';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';

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

  createQuestion(question: Question) {
    return this.http.post(this.apiUrl, question);
  }

  updateQuestion(id: number, question: Question) {
    console.log("Updating question with ID:", question.id);
    return this.http.put(`${this.apiUrl}/${id}/edit`, question);
  }

  updateStatus(id: number) {
    return this.http.patch(`${this.apiUrl}/${id}/status`, id);
  }
}
