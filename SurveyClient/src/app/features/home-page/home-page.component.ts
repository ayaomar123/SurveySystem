import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { HomeService } from './services/home.service';
import { Statics, SurveyAnalytics } from './interfaces/home';
import { QuestionTypeNamePipe } from "../questions/pipes/question-type-name.pipe";
import { ɵInternalFormsSharedModule } from "@angular/forms";

@Component({
  selector: 'app-home-page',
  imports: [CommonModule, QuestionTypeNamePipe, ɵInternalFormsSharedModule],
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
  getStarColor(star: string): string {
    switch (star) {
      case "1": return 'bg-gradient-to-r from-red-500 to-red-700';
      case '2': return 'bg-gradient-to-r from-orange-400 to-orange-600';
      case "3": return 'bg-gradient-to-r from-yellow-400 to-yellow-500';
      case "4": return 'bg-gradient-to-r from-green-400 to-green-600';
      case "5": return 'bg-gradient-to-r from-blue-500 to-blue-700';
      default: return 'bg-gradient-to-r from-gray-400 to-gray-600';
    }
  }

}
