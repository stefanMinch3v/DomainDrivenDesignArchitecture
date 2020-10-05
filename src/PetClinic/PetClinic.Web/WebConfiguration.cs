namespace PetClinic.Web
{
    using Application.Common;
    using Application.Common.Contracts;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Services;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(this IServiceCollection services)
        {
            services
                .AddScoped<ICurrentUser, CurrentUserService>()
                .AddControllers()
                .AddFluentValidation(validation => validation
                    .RegisterValidatorsFromAssemblyContaining<Result>())
                .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerOptions();

            return services;
        }

        public static IApplicationBuilder AddSwaggerUI(this IApplicationBuilder app)
            => app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/all/swagger.json", "All versions");
                    options.DisplayRequestDuration();
                    options.EnableDeepLinking();
                    options.DisplayOperationId();
                });

        private static void AddSwaggerOptions(this IServiceCollection services)
            => services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(
                        "all",
                        new OpenApiInfo
                        {
                            Title = "PetClinic API",
                            Version = "All"
                        });

                    options.AddJwtToSwagger();

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);

                    options.CustomSchemaIds(type => type.FullName);
                });

        private static void AddJwtToSwagger(this SwaggerGenOptions swagger)
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = $"Authorization header using the 'Bearer' scheme.\n\rEnter 'Bearer' [space] and then your token in the text input below.\r\nExample: 'Bearer eyJuYW1laWQi'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer",
                BearerFormat = "Bearer"
            });

            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        },
                    },
                    new List<string>()
                }
            });
        }
    }
}
