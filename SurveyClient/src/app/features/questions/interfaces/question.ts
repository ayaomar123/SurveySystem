export interface Question {
  id: string;
  title: string;
  description: string;
  questionType: number;
  isRequired: boolean;
  status: boolean;
  createdAt: Date | string;
  choices?: QuestionChoice[];
  sliderConfig?: Config;
  starConfig?: StarConfig;
}

export interface QuestionChoice {
  id?: string;
  text: string;
  order: number;
}
export interface Config {
  min: number;
  max: number;
  step: number;
  unitLabel: string;
}
export interface StarConfig {
  maxStar: number;
}



