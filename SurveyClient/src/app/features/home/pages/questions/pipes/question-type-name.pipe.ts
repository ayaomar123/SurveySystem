import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'questionTypeName'
})
export class QuestionTypeNamePipe implements PipeTransform {

  transform(value: number): string {
    switch (value) {
      case 0: return 'Text Input';
      case 1: return 'Radio';
      case 2: return 'Checkbox';
      case 3: return 'Yes or No';
      case 4: return 'Rating';
      case 5: return 'Slider';
      default: return 'Unknown';
    }
  }

}
