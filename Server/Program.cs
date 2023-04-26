namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCors(options => // policy der slår CORS fra, så der er adgang fra alle sources
            {
                options.AddPolicy("policy",
                                policy =>
                                {
                                    policy.AllowAnyOrigin();
                                    policy.AllowAnyHeader();
                                    policy.AllowAnyMethod();
                                }
                                );
            }) ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseCors("policy"); // Anvender ovenstående policy

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}