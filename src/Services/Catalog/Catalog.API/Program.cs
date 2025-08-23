using Catalog.API;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices().AddPipeline();
app.Run();

