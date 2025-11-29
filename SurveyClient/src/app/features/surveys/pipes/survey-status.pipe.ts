import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'surveyStatus'
})
export class SurveyStatusPipe implements PipeTransform {

  transform(value: number): string {
    switch (value) {
      case 0: return 'Draft';
      case 1: return 'Active';
      case 2: return 'Closed';
      case 3: return 'Archived';
      default: return 'Draft';
    }
  }

}
