using FluentValidation;

namespace DbChatBot.Application.Schema.Queries.GenerateSchema;

public class GenerateSchemaValidator : AbstractValidator<GenerateSchemaQuery>
{
    public GenerateSchemaValidator()
    {
        RuleFor(x => x.ConnectionString)
            .NotEmpty()
            .NotNull()
            .WithMessage("Connection string is required");
        RuleFor(x => x.DatabaseType)
            .NotEmpty()
            .NotNull()
            .WithMessage("Database type is required");
    }
}