import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SurveyService } from './services/survey.service';
import { Survey, SurveyCreate, SurveyQuestionOrder, UpdateStatus } from './interfaces/survey';
import { Question } from '../questions/interfaces/question';
import { SurveyStatusPipe } from "./pipes/survey-status.pipe";
import { RouterLink } from "@angular/router";
import { ChangeStatusComponent } from "./change-status/change-status.component";

@Component({
  selector: 'app-surveys',
  imports: [CommonModule, ReactiveFormsModule, SurveyStatusPipe, RouterLink, ChangeStatusComponent],
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
  showModal = false;
  selectedSurvey: any = null;

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
      startDate: [null],
      endDate: [null],
      questions: [[]]
    });
  }

  loadSurveys() {
    this.service.loadSurveys().subscribe({
      next: res => this.surveys = res
    });
  }

  loadQuestions() {
    this.service.loadQuestions().subscribe({
      next: res => this.questions = res
    });
  }

  onSubmit() {
    if (this.addForm.invalid) return;

    const payload: SurveyCreate = {
      ...this.addForm.value,
      questions: this.selectedQuestions,
      status: Number(this.addForm.value.status),
    };

    console.log('Payload to submit:', payload);
    const request =
      payload.id === 0
        ? this.service.createSurvey(payload)
        : this.service.updateSurvey(payload.id, payload);

    request.subscribe({
      next: () => {
        this.loadSurveys();
        this.clearForm();
      },
      error: err => {
        alert('Error submitting survey:' + err.error.errors);
      }
    });
  }
  onEdit(survey: Survey, formElement: HTMLElement) {
    this.isEditing = true;
    formElement.classList.remove('hidden');

    this.selectedQuestions = (survey.questions ?? []).map(q => ({
      questionId: q.questionId,
      order: q.order
    }));

    this.addForm.patchValue({
      ...survey,
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

  toggleQuestion(questionId: string, event: Event) {
    const checked = (event.target as HTMLInputElement).checked;
    if (checked) {
      this.addQuestion(questionId);
    } else {
      this.removeQuestion(questionId);
    }
  }

  isChecked(questionId: string): boolean {
    return this.selectedQuestions.some(q => q.questionId === questionId);
  }

  private addQuestion(questionId: string) {
    this.selectedQuestions.push({ questionId, order: this.selectedQuestions.length + 1 });
  }

  private removeQuestion(questionId: string) {
    this.selectedQuestions = this.selectedQuestions.filter(q => q.questionId !== questionId);
  }


  getQuestionOrder(questionId: string): number | null {
    return this.selectedQuestions.find(q => q.questionId === questionId)?.order ?? null;
  }

  toggleModal(survey: any) {
    this.selectedSurvey = survey;
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.selectedSurvey = null;
  }

  saveStatus(data: UpdateStatus) {
    const payload: UpdateStatus = {
      id: data.id,
      status: Number(data.status),
      startDate: data.startDate || null,
      endDate: data.endDate || null
    };

    this.service.updateSurveyStatus(payload).subscribe({
      next: () => this.loadSurveys()
    });

    this.closeModal();
  }

}
