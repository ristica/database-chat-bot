using FluentValidation;

namespace DbChatBot.Application.Table.Queries.GetData;

public class GetDataValidator : AbstractValidator<GetDataQuery>
{
    public GetDataValidator()
    {
        RuleFor(x => x.ConnectionString)
            .NotEmpty()
            .NotNull()
            .WithMessage("Connection string is required");
        RuleFor(x => x.SqlQuery)
            .NotEmpty()
            .NotNull()
            .WithMessage("SQL query is required");
        RuleFor(x => x.DatabaseType)
            .NotEmpty()
            .NotNull()
            .WithMessage("Database type is required");
    }
}