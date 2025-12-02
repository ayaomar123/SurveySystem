using SurveySystem.Application.Questionns.Dtos;

public sealed record SurveyQuestionDetailsDto(
        Guid? Id,
        string Title,
        string? Description,
        int QuestionType,
        bool IsRequired,
        List<QuestionChoiceDto>? Choices,
        SliderConfigDto? SliderConfig,
        StarConfigDto? StarConfig
    );