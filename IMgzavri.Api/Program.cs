using IMgzavri.Api.Extensions;
using IMgzavri.Commands;
using IMgzavri.Infrastructure.Db;
using IMgzavri.Queries;
using IMgzavri.Shared.Contracts;
using IMgzavri.Shared.Domain.Models;
using IMgzavri.Shared.ExternalServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//InitializeDatabase(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddJwtAndExternalAuthentication(builder.Configuration);

builder.Services.AddCors();

builder.Services.AddMediator(o =>
{
    o.AddHandlersFromAssemblyOf<Command>();
    o.AddHandlersFromAssemblyOf<Query>();
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IRecommend", Version = "v1" });
    c.CustomSchemaIds(x => x.FullName);
});

builder.Services.AddDbContext<IMgzavriDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IMgzavriDbContext")));
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddSingleton<IMailService, MailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

//app.UseHsts();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

