
using DDDPratica.WebApp.MVC.Extensions.Configuration.PipeLine;
using DDDPratica.WebApp.MVC.Extensions.Configuration.Services;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .InstallServices(builder.Configuration);
  
builder
    .Build()
    .ConfigurePipeline()
    .Run();