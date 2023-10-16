using LivroMente.Domain.Models.BookModel;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Models.PaymentModel;
using LivroMente.Infrastructure.Data;
using LivroMente.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultContext")));

builder.Services.AddScoped<ICategoryBookRepository, CategoryBookRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddAutoMapper(typeof(ApplicationDataContext));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
