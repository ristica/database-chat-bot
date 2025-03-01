using FluentValidation;

namespace DbChatBot.Application.Bot.Queries.GetStatement;

public class GetStatementValidator : AbstractValidator<GetStatementQuery>
{
    public GetStatementValidator()
    {
        RuleFor(x => x.Endpoint)
            .NotEmpty()
            .NotNull()
            .WithMessage("AI endpoint is required");
        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .NotNull()
            .WithMessage("AI provider is required");
        RuleFor(x => x.ModelName)
            .NotEmpty()
            .NotNull()
            .WithMessage("AI model is required");
        RuleFor(x => x.ServiceName)
            .NotEmpty()
            .NotNull()
            .WithMessage("AI service is required");
        RuleFor(x => x.Prompt)
            .NotEmpty()
            .NotNull()
            .WithMessage("User's prompt is required");
        RuleFor(x => x.Schema)
            .NotEmpty()
            .NotNull()
            .WithMessage("Database schema is required");
        RuleFor(x => x.DatabaseType)
            .NotEmpty()
            .NotNull()
            .WithMessage("Database type is required");
    }
}