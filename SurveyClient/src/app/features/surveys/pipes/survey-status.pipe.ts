import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'surveyStatus'
})
export class SurveyStatusPipe implements PipeTransform {

  transform(value: number): string {
    switch (value) {
      case 0: return '<span class="inline-block mt-3 mx-2 px-3 py-1 text-xs rounded-full bg-gray-100 text-gray-400">Draft</span>';
      case 1: return '<span class="inline-block mt-3 mx-2 px-3 py-1 text-xs rounded-full bg-green-100 text-green-700">Active</span>';
      case 2: return '<span class="inline-block mt-3 mx-2 px-3 py-1 text-xs rounded-full bg-red-100 text-red-700">Closed</span>';
      case 3: return '<span class="inline-block mt-3 mx-2 px-3 py-1 text-xs rounded-full bg-fuchsia-100 text-fuchsia-700">Archived</span>';
      default: return '<span class="inline-block mt-3 mx-2 px-3 py-1 text-xs rounded-full bg-gray-100 text-gray-400">Draft</span>';
    }
  }

}
