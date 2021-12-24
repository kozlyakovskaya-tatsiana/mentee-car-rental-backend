﻿using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CarRental.API.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerEnvironment(
            this IServiceCollection services
            )
        {
            services.AddSwaggerGen(с =>
            {
                с.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" });
                с.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                с.AddSecurityRequirement(security);
            });
            return services;
        }
    }
}