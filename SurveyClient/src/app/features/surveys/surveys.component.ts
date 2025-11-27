import { StarConfig } from './../questions/interfaces/question';
import { Survey, SurveyCreate, SurveyQuestionOrder } from './interfaces/survey';
import { Component, OnInit } from '@angular/core';
import { SurveyService } from './services/survey.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Question } from '../questions/interfaces/question';

@Component({
  selector: 'app-surveys',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './surveys.component.html',
  styleUrl: './surveys.component.css'
})
export class SurveysComponent implements OnInit {
  surveys: Survey[] = [];
  questions: Question[] = [];
  addForm!: FormGroup;
  isEditing = false;
  selectedQuestions: SurveyQuestionOrder[] = [];
  constructor(private fb: FormBuilder, private service: SurveyService) { }

  ngOnInit() {
    this.initForm();
    this.loadQuestions();
    this.loadSurveys();
  }

  private initForm() {
    this.addForm = this.fb.group({
      id: [0],
      title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
      description: [''],
      status: [0, Validators.required],
      startDate: [new Date()],
      endDate: [new Date()],
      questions: [this.selectedQuestions]
    });
  }

  loadSurveys() {
    this.service.loadSurveys().subscribe({
      next: surveys => this.surveys = surveys
    });
  }

  loadQuestions() {
    this.service.loadQuestions().subscribe({
      next: questions => this.questions = questions
    });
  }


  onSubmit() {
    const data: SurveyCreate = {
      id: this.addForm.value.id,
      title: this.addForm.value.title,
      description: this.addForm.value.description,
      status: Number(this.addForm.value.status),
      startDate: this.addForm.value.startDate,
      endDate: this.addForm.value.endDate,
      questions: this.selectedQuestions,
    };
    console.log(data)

    const request =
      data.id === 0
        ? this.service.createSurvey(data)
        : this.service.updateSurvey(data);

    request.subscribe({
      next: () => {
        this.loadSurveys();
        this.clearForm();
      }
    });
  }

  onEdit(survey: Survey, formElement: HTMLElement) {
    this.isEditing = true;
    formElement.classList.remove('hidden');

    this.clearForm();

    this.selectedQuestions = (survey.questions ?? []).map(q => ({
      questionId: q.questionId,
      order: q.order
    }));

    this.addForm.patchValue({
      id: survey.id,
      title: survey.title,
      description: survey.description,
      status: survey.status,

      startDate: survey.startDate
        ? new Date(survey.startDate).toISOString().substring(0, 10)
        : null,

      endDate: survey.endDate
        ? new Date(survey.endDate).toISOString().substring(0, 10)
        : null,

      questions: this.selectedQuestions
    });


  }


  clearForm() {
    this.isEditing = false;

    this.addForm.reset({
      id: 0,
      title: '',
      description: '',
      status: 0,
      startDate: null,
      endDate: null,
      questions: []
    });

    this.selectedQuestions = [];
  }

  isSelected(questionId: string): boolean {
    return this.selectedQuestions.some(q => q.questionId === questionId);
  }

  getOrder(questionId: string): number | null {
    const q = this.selectedQuestions.find(q => q.questionId === questionId);
    return q ? q.order : null;
  }
  toggleQuestionSelection(questionId: string, event: any) {
    if (event.target.checked) {
      this.selectedQuestions.push({ questionId, order: 1 });
    } else {
      this.selectedQuestions = this.selectedQuestions.filter(q => q.questionId !== questionId);
    }
  }

  setOrder(questionId: string, event: any) {
    const index = this.selectedQuestions.findIndex(q => q.questionId === questionId);
    if (index >= 0) this.selectedQuestions[index].order = Number(event.target.value);
  }
}
