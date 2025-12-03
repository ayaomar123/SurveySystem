export interface Survey {
  id: number;
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


export interface SurveySubmit {
  answers: SurveyAnswers[];
}

export interface SurveyAnswers {
  questionId: string;
  value?: string | null;
  selectedChoiceId?: string | null;
  selectedChoices?: string[] | null;
}


export interface UpdateStatus {
  id: number,
  status: number;
  startDate?: Date | null;
  endDate?: Date | null;
}
