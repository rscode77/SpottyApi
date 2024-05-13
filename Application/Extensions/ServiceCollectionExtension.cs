using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Application.Validators;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataLoggerService, DataLoggerService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IUserActivityService, UserActivityService>();

            AddFluentValidationServices(services);
            AddAutoMapper(services);
        }

        private static void AddFluentValidationServices(IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(provider =>
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ProductionAppMappingProfile());
                }).CreateMapper()
            );
        }
    }
}