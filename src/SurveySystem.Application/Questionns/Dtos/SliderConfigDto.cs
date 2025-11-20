namespace SurveySystem.Application.Questionns.Dtos
{
    public sealed record SliderConfigDto(
    int Min,
    int Max,
    int Step,
    string? UnitLabel
);
}
