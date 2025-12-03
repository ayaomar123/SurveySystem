import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-change-status',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './change-status.component.html',
  styleUrl: './change-status.component.css'
})
export class ChangeStatusComponent {
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
