namespace SurveySystem.Application.Interfaces
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
    }
}
