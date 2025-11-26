export interface Survey {
  id: string;
  title: string;
  description: string;
  status: number;
  startDate: Date;
  endDate: Date;
  createdAt: Date;
  lastModifiedDate: Date;
  createdBy: number;
  questionsCount: number;
  responsesCount: number;
  createdByUser: string | null;
  lastModifiedBy: number | null;
  lastModifiedByUser: string | null;
  questions?: SurveyQuestionOrder[];
}

export interface SurveyCreate {
  id: number;
  title: string;
  description: string;
  status: number;
  startDate: Date;
  endDate: Date;
  questions: SurveyQuestionOrder[];
}

export interface SurveyQuestionOrder {
  questionId: string;
  order: number;
}
