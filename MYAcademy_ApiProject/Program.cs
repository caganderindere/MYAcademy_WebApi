
using MYAcademy_ApiProject.Context;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý
builder.Services.AddDbContext<ApiContext>();

// CORS ayarlarý
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebUI", policy =>
    {
        policy
            .WithOrigins("https://localhost:7118")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Controller ve Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS, controller endpointlerinden önce çalýþmalý
app.UseCors("WebUI");

app.UseAuthorization();

app.MapControllers();

app.Run();
