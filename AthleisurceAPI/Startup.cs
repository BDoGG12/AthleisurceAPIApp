using AthleisurceAPI.Service;

namespace AthleisurceAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddScoped<CartService>(_ => new CartService("MongoAthleisurceDB", "Athleisurce_Cart"));
            services.AddScoped<OrderService>(_ => new OrderService("MongoAthleisurceDB", "Athleisurce_Order"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

                services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder.WithOrigins("http://localhost:3000")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());
                });


            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
