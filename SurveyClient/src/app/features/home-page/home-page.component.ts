import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { HomeService } from './services/home.service';
import { Statics, SurveyAnalytics } from './interfaces/home';
import { QuestionTypeNamePipe } from "../questions/pipes/question-type-name.pipe";

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, QuestionTypeNamePipe],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit {

  today = new Date();
  statics: Statics | undefined;
  analytics: SurveyAnalytics | undefined;
  showAnalyticsDiv: boolean = false;
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

  getSurveyStatics(event: any) {
    console.log(event.target.value);
    var id = event.target.value

    this.service.getAnalytics(id).subscribe({
      next: res => {
        console.log(res)
        this.analytics = res,
          this.showAnalyticsDiv = true
      }
    });
  }
}
