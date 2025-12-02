import { Injectable, inject } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { CreateUser, User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${environment.apiUrl}/Users`;
  private http = inject(HttpClient);

  loadUsers() {
    return this.http.get<User[]>(this.apiUrl);
  }

  createUser(data: CreateUser) {
    return this.http.post<User>(this.apiUrl, data);
  }

  updateUser(id: number, data: CreateUser) {
    return this.http.put<User>(`${this.apiUrl}/${id}`, data);
  }
}
