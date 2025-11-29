using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Domain.Entites.Surveys
{
    public class Survey
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public string? Description { get; private set; }
        public SurveyStatus Status { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }
        public Guid CreatedBy { get; private set; }
        public User? CreatedByUser { get; private set; }
        public Guid? LastModifiedBy { get; private set; }
        public User? LastModifiedByUser { get; private set; }
        public ICollection<SurveyQuestion> SurveyQuestions { get; private set; } = new List<SurveyQuestion>();

        private Survey() { }

        private Survey(
            string title,
            string? description,
            SurveyStatus status,
            DateTime? startDate,
            DateTime? endDate,
            Guid createdBy)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }
        public static Survey Create(
            string title,
            string? description,
            SurveyStatus status,
            DateTime? startDate,
            DateTime? endDate,
            Guid createdBy)
        {
            return new Survey(title, description, status, startDate, endDate, createdBy);
        }

        public void Update(
            string title,
            string? description,
            DateTime? startDate,
            DateTime? endDate,
            Guid modifiedBy)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;

            LastModifiedBy = modifiedBy;
            LastModifiedDate = DateTime.UtcNow;
        }
        public void UpdateStatus(
            SurveyStatus status,
            Guid modifiedBy,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            Status = status;
            LastModifiedBy = modifiedBy;
            LastModifiedDate = DateTime.UtcNow;

            if (status == SurveyStatus.Active)
            {
                StartDate = startDate!;
                EndDate = endDate!;
            }
            else
            {
                StartDate = null;
                EndDate = null;
            }
        }

        
        public void AddQuestion(Guid questionId, int order)
        {
            SurveyQuestions.Add(new SurveyQuestion(Id, questionId, order));
        }
    }
}
