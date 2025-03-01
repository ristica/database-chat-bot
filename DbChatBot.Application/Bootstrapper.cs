using DbChatBot.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DbChatBot.Application;

public static class Bootstrapper
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(
            options =>
            {
                // commands / queries
                options.RegisterServicesFromAssembly(typeof(Bootstrapper).Assembly);

                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

        // validators
        services.AddValidatorsFromAssemblyContaining(typeof(Bootstrapper));

        // auto mapper
        services.AddAutoMapper(typeof(Bootstrapper));

        return services;
    }
}