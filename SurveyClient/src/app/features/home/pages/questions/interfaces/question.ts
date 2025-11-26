export interface Question {
  id: number;
  title: string;
  description: string;
  questionType: number;
  isRequired: boolean;
  status: boolean;
  createdAt: Date | string;
  choices?: QuestionChoice[];
  sliderConfig?: SliderConfig;
  starConfig?: StarConfig;
}

export interface QuestionChoice {
  id?: number;
  text: string;
  order: number;
}
export interface SliderConfig {
  min: number;
  max: number;
  step: number;
  unitLabel: string;
}
export interface StarConfig {
  maxStar: number;
}



