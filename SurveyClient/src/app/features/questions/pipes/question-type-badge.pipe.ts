import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'questionTypeBadge'
})
export class QuestionTypeBadgePipe implements PipeTransform {

  transform(value: number): string {
    switch (value) {
      case 0: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-indigo-100 text-indigo-700 rounded-full">Text Input</span>';
      case 1: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-green-100 text-green-700 rounded-full">Radio</span>';
      case 2: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-blue-100 text-gray-400 rounded-full">Checkbox</span>';
      case 3: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-purple-100 text-purple-700 rounded-full">Yes or No</span>';
      case 4: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-pink-100 text-pink-700 rounded-full">Rating</span>';
      case 5: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-orange-200 text-orange-400 rounded-full">Slider</span>';
      default: return '<span class="inline-block mt-3 px-3 py-1 text-xs bg-gray-100 text-gray-700 rounded-full">Unknown</span>';
    }

  }
}
