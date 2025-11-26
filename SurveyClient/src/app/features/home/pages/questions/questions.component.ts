import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { QuestionService } from './Services/question.service';
import { Question, QuestionChoice } from './interfaces/question';
import { QuestionTypeNamePipe } from "./pipes/question-type-name.pipe";

@Component({
  selector: 'app-questions',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, QuestionTypeNamePipe],
  templateUrl: './questions.component.html',
  styleUrl: './questions.component.css'
})
export class QuestionsComponent implements OnInit {

  questions: Question[] = [];
  questionForm!: FormGroup;
  isEditing = false;

  constructor(private fb: FormBuilder, private service: QuestionService) { }

  ngOnInit() {
    this.initForm();
    this.loadQuestions();
  }

  private initForm() {
    this.questionForm = this.fb.group({
      id: [0],
      title: ['', Validators.required],
      description: [''],
      questionType: [0, Validators.required],
      isRequired: [false],

      choices: this.fb.array([]),

      maxStar: [null],
      sliderMin: [null],
      sliderMax: [null],
      sliderStep: [null],
      sliderUnit: ['']
    });
  }

  get choices() {
    return this.questionForm.get('choices') as FormArray;
  }

  addChoice() {
    this.choices.push(new FormGroup({
      text: new FormControl('',),
      order: new FormControl(1)
    }));
  }

  removeChoice(i: number) {
    this.choices.removeAt(i);
  }

  loadQuestions() {
    this.service.loadQuestions().subscribe({
      next: questions => this.questions = questions
    });
  }

  onSubmit() {
    const dto = this.questionForm.value;

    const request$ =
      dto.id === 0
        ? this.service.createQuestion(dto)
        : this.service.updateQuestion(dto);

    request$.subscribe({
      next: () => {
        this.loadQuestions();
        this.clearForm();
      }
    });
  }

  onEdit(question: Question, formElement: HTMLElement) {
    this.isEditing = true;
    console.log(question);
    formElement.classList.remove('hidden');

    this.clearForm();

    this.questionForm.patchValue({
      id: question.id,
      title: question.title,
      description: question.description,
      questionType: question.questionType,
      isRequired: question.isRequired,
      maxStar: question.starConfig?.maxStar ?? null,
      sliderMin: question.sliderConfig?.min ?? null,
      sliderMax: question.sliderConfig?.max ?? null,
      sliderStep: question.sliderConfig?.step ?? null,
      sliderUnit: question.sliderConfig?.unitLabel ?? ''
    });

    if (question.choices?.length) {
      question.choices.forEach(opt => {
        this.choices.push(
          this.fb.group({
            text: [opt.text],
            order: [opt.order]
          })
        );
      });
    }
  }

  toggleStatus(id: number) {
    this.service.updateStatus(id).subscribe({
      next: () => this.loadQuestions()
    });
  }

  clearForm() {
    this.isEditing = false;

    this.questionForm.reset({
      id: 0,
      title: '',
      description: '',
      questionType: 0,
      isRequired: false,

      maxStar: null,
      sliderMin: null,
      sliderMax: null,
      sliderStep: null,
      sliderUnit: ''
    });

    this.choices.clear();
  }
}
