using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestionStatus
{
    public class UpdateQuestionStatusCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateQuestionStatusCommand, bool>
    {
        public async Task<bool> Handle(UpdateQuestionStatusCommand request, CancellationToken ct)
        {
            var question = await context.Questions
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (question == null)
                throw new Exception("not found");

            question.UpdateStatus();

            await context.SaveChangesAsync(ct);

            return true;
        }
    }
}
