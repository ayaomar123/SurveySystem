namespace SurveySystem.Domain.Entites.Surveys.Responses
{
    public class SurveyResponse
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid SurveyId { get; private set; }
        public string? IpAddress { get; private set; }
        public string? UserAgent { get; private set; }
        public DateTime SubmittedAt { get; private set; } = DateTime.UtcNow;

        public List<SurveyAnswer> Answers { get; private set; } = new();

        private SurveyResponse() { }

        public SurveyResponse(Guid surveyId, string? ipAddress, string? userAgent)
        {
            SurveyId = surveyId;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        public void AddAnswer(SurveyAnswer answer)
        {
            Answers.Add(answer);
        }
    }
}
