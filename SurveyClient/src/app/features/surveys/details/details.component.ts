import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormControl, FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from '../services/survey.service';

@Component({
  selector: 'app-details',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css'
})
export class DetailsComponent implements OnInit {

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
        console.log("Survey Data:", res);
        this.survey = res;
        this.buildForm();
      }
    });
  }

  /** Build Form Dynamically */
  buildForm() {
    const group: any = {};

    this.survey.questions.forEach((q: any) => {

      // CHECKBOX = FormArray
      if (q.questionType === 2) {
        group[q.id] = this.fb.array([]);
      }

      // SLIDER = numeric starting value
      else if (q.questionType === 5) {
        group[q.id] = new FormControl(q.sliderConfig?.min ?? 0);
      }

      // STAR Rating
      else if (q.questionType === 4) {
        group[q.id] = new FormControl(0);
      }

      // TEXT INPUT
      else if (q.questionType === 0) {
        group[q.id] = new FormControl('');
      }

      // YES/NO or RADIO
      else {
        group[q.id] = new FormControl(null);
      }
    });

    this.form = this.fb.group(group);
  }

  /** Handle checkbox logic */
  onCheckboxChange(qid: string, value: string, event: Event) {
    const checkbox = event.target as HTMLInputElement;
    const formArray = this.form.get(qid) as FormArray;

    if (checkbox.checked) {
      formArray.push(new FormControl(value));
    } else {
      const index = formArray.controls.findIndex(c => c.value === value);
      formArray.removeAt(index);
    }
  }

  /** Star Rating */
  setRating(qid: string, rating: number) {
    this.form.get(qid)?.setValue(rating);
  }

  onSubmit() {
    console.log("Submitted Answers:");
    console.log(this.form.value);

  }
}
