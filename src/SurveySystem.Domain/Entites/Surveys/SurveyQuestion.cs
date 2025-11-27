using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Domain.Entites.Surveys
{
    public sealed class SurveyQuestion
    {
        public Guid Id { get; private set; }
        public Guid SurveyId { get; private set; }
        public Survey Survey { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question? Question { get; private set; }
        public int Order { get; private set; }

        private SurveyQuestion() { }

        public SurveyQuestion(Guid surveyId, Guid questionId, int order)
        {
            Id = Guid.NewGuid();
            SurveyId = surveyId;
            QuestionId = questionId;
            Order = order;
        }


        public static SurveyQuestion CreateSurveyQuestion(
            Guid surveyId, Guid questionId, int order)
        {
            return new SurveyQuestion(surveyId, questionId, order);
        }
        public void UpdateOrder(int newOrder)
        {
            Order = newOrder;
        }
    }
}
