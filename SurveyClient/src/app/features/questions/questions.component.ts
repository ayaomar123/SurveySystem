import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormArray, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { QuestionService } from './Services/question.service';
import { Question } from './interfaces/question';
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
    return this.questionForm.get('choices') as FormArray;
  }

  addChoice() {
    this.choices.push(
      this.fb.group({
        text: [''],
        order: [1]
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
    const dto = this.questionForm.value;

    let payload: any = {
      title: dto.title,
      description: dto.description,
      isRequired: dto.isRequired,
      questionType: Number(dto.questionType),
    };

    if (dto.questionType == 1 || dto.questionType == 2) {
      payload.choices = dto.choices;
    }
    if (dto.questionType == 4) {
      payload.starConfig = {
        maxStar: dto.starConfig.maxStar
      };
    }

    if (dto.questionType == 5) {
      payload.config = {
        min: dto.config.min,
        max: dto.config.max,
        step: dto.config.step,
        unitLabel: dto.config.unitLabel
      };
    }

    const request =
      dto.id === 0
        ? this.service.createQuestion(payload)
        : this.service.updateQuestion(dto.id, payload);

    request.subscribe({
      next: () => {
        this.loadQuestions();
        this.clearForm();
      }
    });
  }

  onEdit(question: Question, formElement: HTMLElement) {
    console.log("Editing Question:", question);
    this.isEditing = true;
    formElement.classList.remove('hidden');

    this.clearForm();

    this.questionForm.patchValue({
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


    console.log(this.questionForm.value.id);
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

    this.questionForm.reset({
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
