import { Injectable, OnInit } from '@angular/core';
import { Question } from '../interfaces/question';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { catchError, of, tap } from 'rxjs';

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
    return this.http
      .get<Question[]>(this.apiUrl)
      .pipe(
        catchError(error => {
          console.error(error);
          return of(null);
        }),
        tap(response => {
          if (!response) return;
          const data = response as Question[];
        })
      );
  }

  createQuestion(question: Question) {
    return this.http.post(this.apiUrl, question);
  }

  updateQuestion(id: string, question: Question) {
    return this.http.put(`${this.apiUrl}/${id}/edit`, question);
  }

  updateStatus(id: string) {
    return this.http.patch(`${this.apiUrl}/${id}/status`, id);
  }
}
