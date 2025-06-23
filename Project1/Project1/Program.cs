using Microsoft.AspNetCore.Authentication;
using Project1.Application.Contracts;
using Project1.Shared.Constants;
using Project1.Presentation.Auth.CurrentUsers;
using Project1.Presentation.Auth.Handlers;
using Project1.Application.UseCases.GetProfile;
using Project1.Domain.Repos;
using Project1.Infrastructure.Persistence.Repos;
using Project1.Infrastructure.Persistence.Contracts;
using Project1.Infrastructure.Domain.Repos;
using Project1.Application.UseCases.GetAllProfiles;

var builder = WebApplication.CreateBuilder(args);

registerDi(builder.Services);

var app = builder.Build();

SetupApp(app);

app.Run();

// Add services to the container.
static void registerDi(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(SetupSwaggerOptions);

    services.AddAuthentication(Auth.DefaultScheme)
        .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(Auth.DefaultScheme, (options) => { });

    services.AddAuthorization();

    services.AddHttpContextAccessor();
    services.AddScoped<ICurrentUser, HttpContextCurrentUser>();

    services.AddScoped<GetProfileUseCase>();
    services.AddScoped<GetAllProfilesUseCase>();
    services.AddScoped<IUserRepo, UserRepo>();
    services.AddScoped<IUserModelRepo, InMemoryUserModelRepo>();
}

static void SetupSwaggerOptions(Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions options)
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mock Profile API Service",
        Version = "v1"
    });

    // Define custom auth scheme
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Enter token in this format: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = Auth.DefaultScheme,
    });

    // Apply security globally to all endpoints
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
}

static void SetupApp(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}
