import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from "@angular/router";
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { User } from '../../auth/user.interface';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(public auth: AuthService) { }
  user: User | null = null;

  onLogout() {
    this.auth.logout();
  }

  ngOnInit(): void {
    this.user = this.auth.user();
    console.log(this.user);
  }
}
