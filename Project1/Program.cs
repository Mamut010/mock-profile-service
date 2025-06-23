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
    services.AddSwaggerGen();

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
