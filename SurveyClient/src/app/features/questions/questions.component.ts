import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { QuestionService } from './Services/question.service';
import { Question } from './interfaces/question';
import { QuestionTypeBadgePipe } from './pipes/question-type-badge.pipe';

@Component({
  selector: 'app-questions',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, QuestionTypeBadgePipe],
  templateUrl: './questions.component.html',
  styleUrl: './questions.component.css'
})
export class QuestionsComponent implements OnInit {

  questions: Question[] | null = [];
  addForm!: FormGroup;
  isEditing = false;
  constructor(private fb: FormBuilder, private service: QuestionService) { }

  ngOnInit() {
    this.initForm();
    this.loadQuestions();
  }

  private initForm() {
    this.addForm = this.fb.group({
      id: [0],
      title: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(1000)]],
      description: [null, [Validators.minLength(3), Validators.maxLength(1000)]],
      questionType: [0, Validators.required],
      isRequired: [true],
      choices: this.fb.array([]),
      starConfig: this.fb.group({
        maxStar: [null]
      }),
      config: this.fb.group({
        min: [null],
        max: [null],
        step: [null],
        unitLabel: [null]
      })
    });
  }

  get choices() {
    return this.addForm.get('choices') as FormArray;
  }

  addChoice() {
    this.choices.push(
      this.fb.group({
        text: [''],
        order: [this.choices.length + 1]
      })
    );
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
    const data = this.addForm.value;

    let payload: any = {
      title: data.title,
      description: data.description,
      isRequired: data.isRequired,
      questionType: Number(data.questionType),
    };
    if (data.questionType == 1 || data.questionType == 2) {
      payload.choices = data.choices;
    }
    if (data.questionType == 4) {
      payload.starConfig = {
        maxStar: data.starConfig.maxStar
      };
    }
    if (data.questionType == 5) {
      payload.config = {
        min: data.config.min,
        max: data.config.max,
        step: data.config.step,
        unitLabel: data.config.unitLabel
      };
    }

    const request =
      data.id === 0
        ? this.service.createQuestion(payload)
        : this.service.updateQuestion(data.id, payload);

    request.subscribe({
      next: () => {
        this.loadQuestions();
        this.clearForm();
      }
    });
  }

  onEdit(question: Question, formElement: HTMLElement) {
    this.isEditing = true;
    formElement.classList.remove('hidden');

    this.addForm.patchValue({
      id: question.id,
      title: question.title,
      description: question.description,
      questionType: question.questionType,
      isRequired: question.isRequired,
      starConfig: {
        maxStar: question.starConfig?.maxStar ?? null
      },
      config: {
        min: question.sliderConfig?.min ?? null,
        max: question.sliderConfig?.max ?? null,
        step: question.sliderConfig?.step ?? null,
        unitLabel: question.sliderConfig?.unitLabel ?? null
      }
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

  toggleStatus(id: string) {
    this.service.updateStatus(id).subscribe({
      next: () => this.loadQuestions()
    });
  }

  clearForm() {
    this.isEditing = false;

    this.addForm.reset({
      id: 0,
      title: '',
      description: '',
      questionType: 0,
      isRequired: false,
      starConfig: {
        maxStar: null
      },
      config: {
        min: null,
        max: null,
        step: null,
        unitLabel: null
      }
    });

    this.choices.clear();
  }
}
