using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using System.IO;
using dotenv.net;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Carregar variáveis de ambiente do arquivo .env
        DotEnv.Load();

        string? server = Environment.GetEnvironmentVariable("SERVER");
        string? database = Environment.GetEnvironmentVariable("DATABASE");
        string? username = Environment.GetEnvironmentVariable("USERNAME");
        string? password = Environment.GetEnvironmentVariable("PASSWORD");

        builder.Services.AddSingleton<IDatabaseConnection>(provider =>
        {
            if (server == null || database == null || username == null || password == null)
            {
                throw new InvalidOperationException("Variáveis de ambiente não configuradas corretamente.");
            }

            return new MySqlConnectionAdapter(server, database, username, password);
        });

        // Registro da implementação de IUserService
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyPass v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
