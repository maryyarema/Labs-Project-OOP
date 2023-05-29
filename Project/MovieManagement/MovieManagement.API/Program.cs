using System.Data;
using System.Data.SqlClient;
using MovieManagement.DAL.Repositories.Contracts;
using MovieManagement.DAL.Repositories;
using MovieManagement.BLL.Services.Consracts;
using MovieManagement.BLL.Services;
using MovieManagement.DAL.Entities;

        var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.


    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


    // Connection/Transaction for ADO.NET/DAPPER database
    builder.Services.AddScoped(provider =>
            new SqlConnection(builder.Configuration.GetConnectionString("sqlConnection")));
    builder.Services.AddScoped<IDbTransaction>(provider =>
    {
        var connection = provider.GetRequiredService<SqlConnection>();
        connection.Open();
        return connection.BeginTransaction();
    });

// Dependendency Injection for Repositories/UOW from ADO.NET DAL
{
    builder.Services.AddScoped<IMovieRepository, MovieRepository>();
    builder.Services.AddScoped<IActorRepository, ActorRepository>();
    builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IMovieCategoryRepository, MovieCategoryRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}
// BLL
{
    builder.Services.AddScoped<IMovieService, MovieService>();
    builder.Services.AddScoped<IActorService, ActorService>();
    builder.Services.AddScoped<IMovieActorService, MovieActorService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IMovieCategoryService, MovieCategoryService>();
 }
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseDeveloperExceptionPage();


app.MapControllers();

app.Run();









