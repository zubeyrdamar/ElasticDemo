
using Elastic.API.Extensions;
using Elastic.API.Repository;
using Elastic.API.Services;
using Elasticsearch.Net;
using Nest;

namespace Elastic.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            |
            |   SERVICES
            |
            */

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Handle Elastic's configurations
            builder.Services.AddElastic(builder.Configuration);

            // Scopes
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductRepository>();

            /*
            |
            |   APP
            |
            */

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
