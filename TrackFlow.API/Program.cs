using TrackFlow.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackFlow API v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();