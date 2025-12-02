import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SurveyService } from './services/survey.service';
import { Survey, SurveyCreate, SurveyQuestionOrder } from './interfaces/survey';
import { Question } from '../questions/interfaces/question';
import { SurveyStatusPipe } from "./pipes/survey-status.pipe";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-surveys',
  imports: [CommonModule, ReactiveFormsModule, SurveyStatusPipe, RouterLink],
  templateUrl: './surveys.component.html',
  styleUrl: './surveys.component.css'
})
export class SurveysComponent implements OnInit {

  surveys: Survey[] = [];
  questions: Question[] = [];
  addForm!: FormGroup;
  statusForm!: FormGroup;

  isEditing = false;
  selectedQuestions: SurveyQuestionOrder[] = [];

  constructor(
    private fb: FormBuilder,
    private service: SurveyService
  ) { }

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
      questions: [[]]
    });
  }

  loadSurveys() {
    this.service.loadSurveys().subscribe({
      next: res => this.surveys = res
    });
  }

  loadQuestions() {
    this.service.loadQuestions({ status: true }).subscribe({
      next: res => this.questions = res
    });
  }

  onSubmit() {
    const payload: SurveyCreate = {
      id: this.addForm.value.id,
      title: this.addForm.value.title,
      description: this.addForm.value.description,
      status: Number(this.addForm.value.status),
      startDate: this.addForm.value.startDate ?? null,
      endDate: this.addForm.value.endDate ?? null,
      questions: this.selectedQuestions
    };

    const request =
      payload.id === 0
        ? this.service.createSurvey(payload)
        : this.service.updateSurvey(payload.id, payload);

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
      startDate: this.formatDate(survey.startDate),
      endDate: this.formatDate(survey.endDate),
      questions: this.selectedQuestions
    });
  }

  

  private formatDate(date: Date | null): string | null {
    return date ? new Date(date).toISOString().substring(0, 10) : null;
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

  onQuestionToggle(questionId: string, event: Event) {
    const checked = (event.target as HTMLInputElement).checked;

    if (checked) {
      this.addQuestion(questionId);
    } else {
      this.removeQuestion(questionId);
    }
  }

  private addQuestion(questionId: string) {
    if (!this.isQuestionSelected(questionId)) {
      this.selectedQuestions.push({ questionId, order: 1 });
    }
  }

  private removeQuestion(questionId: string) {
    this.selectedQuestions = this.selectedQuestions.filter(q => q.questionId !== questionId);
  }

  onOrderChange(questionId: string, event: Event) {
    const input = event.target as HTMLInputElement;
    const q = this.selectedQuestions.find(x => x.questionId === questionId);

    if (q) q.order = Number(input.value);
  }

  isQuestionSelected(questionId: string): boolean {
    return this.selectedQuestions.some(q => q.questionId === questionId);
  }

  getQuestionOrder(questionId: string): number | null {
    return this.selectedQuestions.find(q => q.questionId === questionId)?.order ?? null;
  }

}
