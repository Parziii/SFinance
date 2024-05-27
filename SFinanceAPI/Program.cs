
using Microsoft.EntityFrameworkCore;
using SFinanceAPI.DbContext;

namespace SFinanceAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<SFinanceContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("data source=dsencio-dell\\MSSQLSERVER01;initial catalog=SFinanceDb;trusted_connection=true")));


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
