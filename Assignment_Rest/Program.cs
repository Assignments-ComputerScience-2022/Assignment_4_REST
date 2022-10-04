var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();