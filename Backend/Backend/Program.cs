


using Backend;
using Microsoft.EntityFrameworkCore;
using Backend.Interfaces;
using Backend.Data.Repositories;
using DbContext = Backend.DbContext;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddTransient<IRoomRepository, RoomSqlServerRepository>();
//builder.Services.AddTransient<IRoomTypeRepository, RoomTypeSqlServerRepository>();

builder.Services.AddControllers();


builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}
               );

builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.MapSwagger();
app.UseSwaggerUI();

app.UseCors("LocalPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
