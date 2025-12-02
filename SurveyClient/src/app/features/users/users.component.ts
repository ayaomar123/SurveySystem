import { Component, inject, OnInit } from '@angular/core';
import { UserService } from './services/user.service';
import { CreateUser, User } from './interfaces/user';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-users',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  addForm!: FormGroup;

  isEditing = false;
  private service = inject(UserService);
  private fb = inject(FormBuilder);

  ngOnInit(): void {
    this.getUsers();
    this.initForm();
  }

  getUsers() {
    this.service.loadUsers().subscribe({
      next: res => this.users = res
    });
  }

  private initForm() {
    this.addForm = this.fb.group({
      id: [0],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(200)]],
      email: ['', [Validators.required, Validators.email]],
      passwordHash: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)]],
      role: [null, Validators.required]
    });
  }
  onSubmit() {
    const payload: CreateUser = {
      id: this.addForm.value.id,
      name: this.addForm.value.name,
      email: this.addForm.value.email,
      passwordHash: this.addForm.value.passwordHash,
      role: Number(this.addForm.value.role)
    };
    const request =
      payload.id === 0
        ? this.service.createUser(payload)
        : this.service.updateUser(payload.id, payload);

    request.subscribe({
      next: () => {
        this.getUsers();
        this.clearForm();
      }
    });
  }

  clearForm() {
    this.isEditing = false;
    this.addForm.reset();
  }

  onEdit(user: User, formElement: HTMLElement) {
    this.isEditing = true;
    formElement.classList.remove('hidden');

    this.clearForm();

    this.addForm.patchValue({
      id: user.id,
      name: user.name,
      email: user.email,
      passwordHash: user.passwordHash,
      role: user.role,
    });
  }
}
