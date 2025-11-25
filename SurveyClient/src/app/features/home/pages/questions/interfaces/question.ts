export interface Question {
  id: number;
  title: string;
  description: string;
  questionType: number;
  isRequired: boolean;
  status: boolean;
  createdAt: Date | string;
  /*QuestionChoice: string;
  SliderConfig: string;
  StarConfig: string;*/
}

