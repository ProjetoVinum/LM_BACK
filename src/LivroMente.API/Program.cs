using System.Text;
using AutoMapper;
using LivroMente.API.Integration;
using LivroMente.API.Integration.Interfaces;
using LivroMente.API.Integration.Refit;
using LivroMente.Domain.Models.AdressModel;
using LivroMente.Domain.Models.BookModel;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Domain.Models.PaymentModel;
using LivroMente.Domain.Models.RoleModel;
using LivroMente.Domain.Models.UserModel;
using LivroMente.Infrastructure.Data;
using LivroMente.Infrastructure.Mapping;
using LivroMente.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIContagem", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });


builder.Services.AddDbContext<ApplicationDataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultContext")));

builder.Services.AddScoped<ICategoryBookRepository, CategoryBookRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAdressRepository, AdressRepository>();
builder.Services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
builder.Services.AddScoped(typeof(IRoleRepository<>), typeof(RoleRepository<>));
builder.Services.AddAutoMapper(typeof(ApplicationDataContext));
builder.Services.AddScoped<IViaCepIntegration, ViaCepIntegration>();
builder.Services.AddRefitClient<IViaCepIntegrationRefit>().ConfigureHttpClient( c => 
{
 c.BaseAddress = new Uri("https://viacep.com.br");
});

builder.Services.AddIdentityCore<User>(options => {
    //options.SignIn.RequireConfirmedEmail = true;

    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true; 
})
.AddRoles<Role>()
.AddEntityFrameworkStores<ApplicationDataContext>()
.AddRoleValidator<RoleValidator<Role>>()
.AddRoleManager<RoleManager<Role>>()
.AddSignInManager<SignInManager<User>>()
.AddDefaultTokenProviders();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts => {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

        };
    });

builder.Services.AddMvc(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    }
);

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfiles());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
