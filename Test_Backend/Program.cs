using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Test_Backend.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContex>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowdreact", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Repositories
builder.Services.AddScoped<Test_Backend.Repositories.ICustomerRepository, Test_Backend.Repositories.CustomerRepository>();
builder.Services.AddScoped<Test_Backend.Repositories.IAuthRepository, Test_Backend.Repositories.AuthRepository>();

// Services
builder.Services.AddScoped<Test_Backend.Services.ICustomerService, Test_Backend.Services.CustomerService>();
builder.Services.AddScoped<Test_Backend.Services.IAuthService, Test_Backend.Services.AuthService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<Test_Backend.Middlewares.GlobalExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("allowdreact");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
