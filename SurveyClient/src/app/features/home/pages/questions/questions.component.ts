import { QuestionService } from './Services/question.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Question } from './interfaces/question';

@Component({
  selector: 'app-questions',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './questions.component.html',
  styleUrl: './questions.component.css'
})
export class QuestionsComponent implements OnInit {
  questions: Question[] = [];
  questionForm: FormGroup;
  isEditing = false;

  constructor(private fb: FormBuilder, private service: QuestionService) {
    this.questionForm = this.fb.group({
      id: [0],
      title: ['', [Validators.required]],
      description: [''],
      questionType: [0, [Validators.required]],
      isRequired: [true],
      status: [true],
      createdAt: ['']
    });
  }

  ngOnInit() {
    this.loadQuestions();
  }

  loadQuestions() {
    this.service.loadQuestions().subscribe({
      next: (questions: Question[]) => {
        this.questions = questions;
      },
    });
  }

  onSubmit() {
    const Question = this.questionForm.value;
    if (Question.id === 0) {
      this.service.createQuestion(Question).subscribe({
        next: res => {
          this.loadQuestions();
          this.questionForm.reset({
            questionType: 0,
            isRequired: false
          });
        },
      });
    } else {
      this.service.updateQuestion(Question).subscribe({
        next: res => {
          this.loadQuestions();
          this.isEditing = false;

          this.questionForm.reset({
            questionType: 0,
            isRequired: false
          });
        },
      });
    }
  }
  onEdit(question: Question, addFormElement: HTMLElement) {
    console.log('Editing question:', question);
    addFormElement.classList.remove('hidden');

    this.isEditing = true;

    this.questionForm.patchValue({
      id: question.id,
      title: question.title,
      description: question.description,
      questionType: question.questionType,
      isRequired: question.isRequired,
      status: question.status,
      createdAt: question.createdAt
    });

  }

  toggleStatus(id: number) {
    this.service.updateStatus(id).subscribe({
      next: res => {
        this.loadQuestions();
      }
    });
  }

}
