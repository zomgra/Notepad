using Microsoft.AspNetCore.Authentication;
using Notepad.API.Schemes;
using Notepad.Application.DependencyInjection;
using Notepad.Storage.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper();
builder.Services.AddMediator();
builder.Services.AddNoteDbContext(builder.Configuration);
builder.Services.AddProviders();
builder.Services.AddRepositories();

builder.Services.AddAuthentication(nameof(CookieHaveUIDScheme))
    .AddScheme<AuthenticationSchemeOptions, CookieHaveUIDScheme>(nameof(CookieHaveUIDScheme), x => { });

builder.Services.AddHttpContextAccessor();

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
