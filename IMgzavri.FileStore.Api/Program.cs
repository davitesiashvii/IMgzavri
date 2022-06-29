using IMgzavri.FileStore.Api.Extensions;
using IMgzavri.FileStore.Commands;
using IMgzavri.FileStore.Domain;
using IMgzavri.FileStore.Infrastructure.Db;
using IMgzavri.FileStore.Infrastructure.Repositories;
using IMgzavri.FileStore.Infrastructure.Services;
using IMgzavri.FileStore.Queries;
using IMgzavri.Shared.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container. 
builder.Services.Configure<IRecommendFileStorageSettings>(builder.Configuration);
var config = builder.Configuration.Get<IRecommendFileStorageSettings>();

//builder.Services.AddControllers()
//                .AddNewtonsoftJson(o =>
//                {
//                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
//                    o.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
//                });


builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddCors();

builder.Services.AddOptions();


builder.Services.Configure<IRecommendFileStorageSettingsGlobalSettings>(builder.Configuration.GetSection("GlobalSettings"));

builder.Services.AddDbContext<IMgzavriFileStorageDbContext>(options => options.UseSqlServer(config.ConnectionStrings.IMgzavriFileStorageDbContext));

builder.Services.InitializeDatabase<IMgzavriFileStorageDbContext>();

builder.Services.AddScoped<IFileStorageRepository, FileStorageRepository>();

builder.Services.AddScoped<IFileProcessor, FileProcessor>();

builder.Services.AddMediator(o =>
{
    o.AddHandlersFromAssemblyOf<Command>();
    o.AddHandlersFromAssemblyOf<Query>();
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseCors(c => c.AllowAnyOrigin()
               .WithOrigins(builder.Configuration.GetSection("GlobalSettings")["Origin"].Split(";"))
               .WithMethods("GET", "POST", "PUT", "DELETE")
               .AllowCredentials()
               .AllowAnyHeader());

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
});

var config1 = builder.Configuration.Get<IRecommendFileStorageSettings>();
app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
    FileProvider = new
        PhysicalFileProvider(Path.Combine(config1.GlobalSettings.FileSystemBasePath, config1.GlobalSettings.MainFolderName)),
    RequestPath = new PathString(config1.GlobalSettings.FileServerRequestPath)
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();





