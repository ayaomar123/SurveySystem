import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { AuthService } from '../../../auth.service';
import { Login } from '../../interfaces';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent implements OnInit {
  private readonly authSvc = inject(AuthService);
  private readonly formBuilder = inject(FormBuilder);
  private readonly destroyRef = inject(DestroyRef);
  loginForm!: FormGroup;

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['ayakhomar@gmail.com', [Validators.required, Validators.email]],
      password: ['123456', Validators.required]
    });
  }

  onLoginFormSubmitted() {
    if (!this.loginForm.valid) {
      return;
    }

    this.authSvc.login(this.loginForm.value as Login).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }
}
