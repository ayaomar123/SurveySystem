namespace SurveySystem.Application.Users.Dtos.Login
{
    public sealed record LoginRequest(
        string Email,
        string Password);
}
