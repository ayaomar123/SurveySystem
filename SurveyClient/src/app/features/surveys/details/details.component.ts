import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormControl, FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from '../services/survey.service';
import { SurveySubmit } from '../interfaces/survey';

@Component({
  selector: 'app-details',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent implements OnInit {
  isActive: boolean = true;
  survey: any = null;
  form!: FormGroup;
  id!: string;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private service: SurveyService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') as string;
    this.loadSurvey();
  }

  loadSurvey() {
    this.service.getSurveyById(this.id).subscribe({
      next: (res) => {
        this.survey = res;
        this.buildForm();
      },
      error: err => {
        if (err.status === 404) {
          this.isActive = false;
        }
      }
    });
  }

  buildForm() {
    const group: any = {};

    this.survey.questions.forEach((q: any) => {

      if (q.questionType === 2) {
        group[q.id] = this.fb.array([]);
      }

      else if (q.questionType === 5) {
        group[q.id] = new FormControl(q.sliderConfig?.min ?? 0);
      }

      else if (q.questionType === 4) {
        group[q.id] = new FormControl(0);
      }

      else if (q.questionType === 0) {
        group[q.id] = new FormControl('');
      }

      else {
        group[q.id] = new FormControl(null);
      }
    });

    this.form = this.fb.group(group);
  }

  onCheckboxChange(qid: string, choiceId: string, event: Event) {
    const checkbox = event.target as HTMLInputElement;
    const formArray = this.form.get(qid) as FormArray;

    if (checkbox.checked) {
      formArray.push(new FormControl(choiceId));
    } else {
      const index = formArray.controls.findIndex(c => c.value === choiceId);
      formArray.removeAt(index);
    }
  }

  setRating(qid: string, rating: number) {
    this.form.get(qid)?.setValue(rating);
  }

  onSubmit() {

    const payload: SurveySubmit = {
      answers: []
    };

    this.survey.questions.forEach((q: any) => {
      const val = this.form.get(q.id)?.value;

      if (q.questionType === 2) {
        payload.answers.push({
          questionId: q.id,
          selectedChoices: val
        });
      }

      else if (q.questionType === 1) {
        payload.answers.push({
          questionId: q.id,
          selectedChoiceId: val
        });
      }

      else {
        payload.answers.push({
          questionId: q.id,
          value: val
        });
      }
    });

    console.log("Payload to API:", payload);

    this.service.submitSurvey(this.id, payload).subscribe({
      next: () => {
        alert("Survey Submitted Successfully!");
        this.buildForm();
      }
    });
  }
}
