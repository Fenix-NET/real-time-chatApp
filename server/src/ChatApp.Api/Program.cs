
using ChatApp.Api.Extensions;
using ChatApp.Application.Services;
using ChatApp.Core.Interfaces;
using ChatApp.Infrastructure.Identity;
using ChatApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigurationCors();
            builder.Services.ConfigurationIdentity();
            builder.Services.ConfigurationAuth(builder.Configuration);

            builder.Services.AddAuthorization();

            builder.Services.ConfigureDb(builder.Configuration);

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<ChatAppDbContext>();
            //    if (dbContext != null) dbContext.Database.Migrate();
            //}

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSockets();

            app.MapControllers();

            app.Run();
        }
    }
}
