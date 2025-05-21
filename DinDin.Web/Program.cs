using DinDin.Domain.Constantes;
using DinDin.Web;

var builder = WebApplication.CreateBuilder(args);

builder.AddServicesInScope();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(ApplicationConstants.CORS_POLICY_NAME);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();