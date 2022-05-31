var app = WebApplication.CreateBuilder(args).Build();
app.MapGet("/", () => "Hello world");
app.Run();
