import { HttpClient, HttpContext } from '@angular/common/http';
import { DestroyRef, inject, Injectable, signal, WritableSignal } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './user.interface';
import { Observable, catchError, of, tap } from 'rxjs';
import { Login, LoginSuccess } from './login/interfaces';
import { LoginResponse } from './login/types/login-response.type';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { IS_PUBLIC } from './auth.interceptor';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly jwtHelper = inject(JwtHelperService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly CONTEXT = { context: new HttpContext().set(IS_PUBLIC, true) };
  private readonly TOKEN_EXPIRY_THRESHOLD_MINUTES = 5;

  get user(): WritableSignal<User | null> {
    const token = localStorage.getItem('token');
    return signal(token ? this.jwtHelper.decodeToken(token) : null);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  isAuthenticated(): boolean {
    return !this.jwtHelper.isTokenExpired();
  }

  login(body: Login): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/Auth/login`, body, this.CONTEXT)
      .pipe(
        catchError(error => {
          if (error.status === 401) {
            console.error('Invalid credentials');
          }
          return of();
        }),
        tap(data => {
          const loginSuccessData = data as LoginSuccess;
          this.storeTokens(loginSuccessData);
          this.router.navigate(['/']);
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }

  storeTokens(data: LoginSuccess): void {
    localStorage.setItem('token', data.token);
  }
}
