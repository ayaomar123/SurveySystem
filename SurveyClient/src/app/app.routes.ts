import { Routes } from '@angular/router';
import { authGuard, accountGuard } from './core/auth/guards';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  {
    path: 'home',
    loadComponent: () => import('./features/home-page/home-page.component').then(mod => mod.HomePageComponent),
    canActivate: [authGuard]
  },
  {
    path: 'questions',
    loadComponent: () => import('./features/questions/questions.component').then(mod => mod.QuestionsComponent),
    canActivate: [authGuard]
  },
  {
    path: 'surveys',
    loadComponent: () => import('./features/surveys/surveys.component').then(mod => mod.SurveysComponent),
    canActivate: [authGuard]
  },
  {
    path: 'surveys/:id',
    loadComponent: () => import('./features/surveys/details/details.component').then(mod => mod.DetailsComponent),
    canActivate: [authGuard],
    data: { noLayout: true }
  },
  {
    path: 'users',
    loadComponent: () => import('./features/users/users.component').then(mod => mod.UsersComponent),
    canActivate: [authGuard]
  },
  {
    path: 'login',
    loadComponent: () => import('./core/auth/login/pages/login-page/login-page.component').then(mod => mod.LoginPageComponent),
    canActivate: [accountGuard]
  }
];
