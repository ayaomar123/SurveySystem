import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { HomeService } from './services/home.service';
import { Statics } from './interfaces/home';

@Component({
  selector: 'app-home-page',
  imports: [CommonModule],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {
  today = new Date();
  statics: Statics | undefined;
  private service = inject(HomeService);

  ngOnInit(): void {
    this.getStats();
  }

  getStats() {
    this.service.loadStatics().subscribe({
      complete: () => console.log(this.statics),
      next: res => this.statics = res
    });
  }
}
