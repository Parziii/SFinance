
using Microsoft.EntityFrameworkCore;
using SFinanceAPI.DbContext;
using SFinanceAPI.Services;
using SFinanceAPI.Services.Interfaces;

namespace SFinanceAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var env = builder.Configuration;

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<SFinanceContext>(options =>
				options.UseSqlServer(env.GetConnectionString("SFinanceDbConnection")));
			
			builder.Services.AddScoped<IFileStorageService, FileStorageService>();
			builder.Services.AddScoped<IOpenAiService, OpenAiService>();


			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddCors();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(policy =>
			{
				policy.AllowAnyOrigin();
				policy.AllowAnyMethod();
				policy.AllowAnyHeader();
			});

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
