import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-status',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './change-status.component.html',
  styleUrl: './change-status.component.css'
})
export class ChangeStatusComponent implements OnChanges {
  @Input() show = false;
  @Input() survey: any | null = null;

  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<any>();

  form!: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      status: ['', Validators.required],
      startDate: [''],
      endDate: ['']
    });
  }

  ngOnChanges() {
    if (this.survey) {
      this.form.patchValue({
        status: this.survey.status,
        startDate: this.survey.startDate,
        endDate: this.survey.endDate,
      });

      this.onStatusChange();
    }
  }

  onStatusChange() {
    const status = this.form.get('status')?.value;

    if (status === '0') {
      this.form.get('startDate')?.setValidators([Validators.required]);
      this.form.get('endDate')?.setValidators([Validators.required]);
    } else {
      this.form.get('startDate')?.clearValidators();
      this.form.get('endDate')?.clearValidators();

      this.form.patchValue({ startDate: null, endDate: null });
    }

    this.form.get('startDate')?.updateValueAndValidity();
    this.form.get('endDate')?.updateValueAndValidity();
  }

  closeModal() {
    this.close.emit();
  }

  saveChanges() {
    if (this.form.invalid) return;

    this.save.emit({
      id: this.survey.id,
      ...this.form.value
    });
  }
}
