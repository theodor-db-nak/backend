using Application.Extensions;
using Infrastructure.Extensions;
using PresentationApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddApplication(builder.Configuration, builder.Environment);

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapApiEndpoints();

app.Run();
