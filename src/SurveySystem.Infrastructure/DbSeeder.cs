using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entites;
using SurveySystem.Domain.Entites.Questions;
using SurveySystem.Domain.Entites.Questions.Enums;
using SurveySystem.Domain.Entites.Surveys;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Infrastructure
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await SeedUserAsync(context);
            await SeedQuestionsAsync(context);
            await SeedSurveysAsync(context);
        }
        public static async Task SeedUserAsync(AppDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var admin = User.Create(
                name: "Aya Al Rahman",
                email: "ayakhomar@gmail.com",
                passwordHash: "123456",
                userRole: UserRole.Admin
            );

            context.Users.Add(admin);
            await context.SaveChangesAsync(); 
        }
        public static async Task SeedQuestionsAsync(AppDbContext context)
        {
            if (await context.Questions.AnyAsync())
                return;

            var questions = new List<Question>
            {
                Question.CreateTextQuestion(
                    title: "What is your name?",
                    description: "Enter your full name",
                    required: true
                ),

                Question.CreateYesNoQuestion(
                    title: "Do you agree with the terms?",
                    description: null,
                    required: true
                ),

                Question.CreateChoiceQuestion(
                    title: "What is your gender?",
                    type: QuestionType.Radio,
                    description: null,
                    required: true,
                    choices: new List<QuestionChoice>
                    {
                        new("Male", 1),
                        new("Female", 2),
                        new("Prefer not to say", 3)
                    }
                ),

                Question.CreateChoiceQuestion(
                    title: "Which programming languages do you know?",
                    type: QuestionType.Checkbox,
                    description: "Select all that apply",
                    required: false,
                    choices: new List<QuestionChoice>
                    {
                        new("C#", 1),
                        new("JavaScript", 2),
                        new("Python", 3),
                        new("Java", 4)
                    }
                ),

                Question.CreateRatingQuestion(
                    title: "How would you rate our service?",
                    description: "1 to 5 stars",
                    required: true,
                    stars: new StarConfig(maxStar: 5)
                ),

                Question.CreateSliderQuestion(
                    title: "How satisfied are you?",
                    description: "From 0 to 100",
                    required: true,
                    config: new SliderConfig(min: 0, max: 100, step: 5, label: "Your Value")
                )
            };

            context.Questions.AddRange(questions);
            await context.SaveChangesAsync();
        }

        public static async Task SeedSurveysAsync(AppDbContext context)
        {
            if (await context.Surveys.AnyAsync())
                return;

            var user = await context.Users.FirstAsync(); 

            var q = await context.Questions.ToListAsync();

            var textQ = q.Find(x => x.QuestionType == QuestionType.TextInput)!;
            var yesNoQ = q.Find(x => x.QuestionType == QuestionType.YesOrNo)!;
            var radioQ = q.Find(x => x.QuestionType == QuestionType.Radio)!;
            var checkboxQ = q.Find(x => x.QuestionType == QuestionType.Checkbox)!;
            var ratingQ = q.Find(x => x.QuestionType == QuestionType.Rating)!;
            var sliderQ = q.Find(x => x.QuestionType == QuestionType.Slider)!;

            var survey1 = Survey.Create(
                title: "Basic Info Survey",
                description: "A survey with one question only",
                status: SurveyStatus.Active,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow.AddMonths(1),
                createdBy: user.Id
            );
            survey1.AddQuestion(textQ.Id, 1);

            var survey2 = Survey.Create(
                title: "Customer Satisfaction Survey",
                description: "Helps us improve our service",
                status: SurveyStatus.Active,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow.AddMonths(1),
                createdBy: user.Id
            );
            survey2.AddQuestion(ratingQ.Id, 1);
            survey2.AddQuestion(sliderQ.Id, 2);
            survey2.AddQuestion(yesNoQ.Id, 3);

            var survey3 = Survey.Create(
                title: "Developer Skills Survey",
                description: "Tell us more about your technical background",
                status: SurveyStatus.Active,
                startDate: DateTime.UtcNow,
                endDate: DateTime.UtcNow.AddMonths(1),
                createdBy: user.Id
            );
            survey3.AddQuestion(checkboxQ.Id, 1);
            survey3.AddQuestion(radioQ.Id, 2);
            survey3.AddQuestion(textQ.Id, 3);
            survey3.AddQuestion(sliderQ.Id, 4);

            context.Surveys.AddRange(survey1, survey2, survey3);

            await context.SaveChangesAsync();
        }
    }
}
