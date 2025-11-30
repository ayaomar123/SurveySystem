
export interface Statics {
  totalResponses: number;
  totalSurveys: number;
  activeSurveys: number;
  totalQuestions: number;
  surveys: Survey[];
}

export interface Survey {
  id: number;
  title: string;
}

export interface SurveyAnalytics {
  title: string;
  totalResponses: number;
  questions: QuestionAnalytics[];
}

export interface QuestionAnalytics {
  questionId: string;
  title: string;
  questionType: number;
  totalResponses: number;
  averageRating: number | null;
  averageSlider: number;
  ratingValues: Record<string, number>[] | null;
  sliderValues: Record<string, number> | null;
  textValues: string | null;
  singleChoiceValues: Record<string, number> | null;
  multipleChoiceValues: Record<string, number> | null;
  yesCount: number | null;
  noCount: number | null;
}
