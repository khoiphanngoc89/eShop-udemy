var builder = WebApplication.CreateBuilder(args);
var app = builder.AddServices().UsePipeline();
app.Run();