import { HttpClient, HttpContext } from '@angular/common/http';
import { inject, Injectable, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { catchError, of, tap } from 'rxjs';
import { Login, LoginSuccess } from './login/interfaces';
import { LoginResponse } from './login/types/login-response.type';
import { IS_PUBLIC } from './auth.interceptor';
import { User } from './user.interface';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly jwt = inject(JwtHelperService);

  private readonly CONTEXT = {
    context: new HttpContext().set(IS_PUBLIC, true)
  };

  private readonly _user = signal<User | null>(null);
  readonly user = this._user.asReadonly();

  constructor() {
    this.initializeUser();
  }

  private initializeUser(): void {
    const token = this.getToken();
    if (!token || this.jwt.isTokenExpired(token)) {
      this._user.set(null);
      return;
    }

    const decoded: any = this.jwt.decodeToken(token);

    // استخراج الـ role من أي مكان
    const role =
      decoded['role'] ||
      decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
      null;

    // تخزين اليوزر داخل الإشارة (signal)
    this._user.set({
      ...decoded,
      role: role
    } as User);

    console.log("Decoded User:", this._user());
  }

  private getToken(): string | null {
    return localStorage.getItem('token');
  }

  private saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  // signals
  isLoggedIn = computed(() => this._user() !== null);
  role = computed(() => this._user()?.role ?? null);

  isAuthenticated(): boolean {
    const token = this.getToken();
    return token !== null && !this.jwt.isTokenExpired(token);
  }

  login(body: Login) {
    return this.http
      .post<LoginResponse>(`${environment.apiUrl}/Auth/login`, body, this.CONTEXT)
      .pipe(
        catchError(error => {
          console.error('Login failed:', error);
          return of(null);
        }),
        tap(response => {
          if (!response) return;

          const data = response as LoginSuccess;

          this.saveToken(data.token);
          this.initializeUser();

          this.router.navigate(['/']);
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    this._user.set(null);
    this.router.navigate(['/login']);
  }
}
