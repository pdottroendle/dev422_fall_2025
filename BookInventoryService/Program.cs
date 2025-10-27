using BookInventoryService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<BookServiceImpl>();
app.MapGet("/", () => "gRPC Book Inventory Service is running.");

app.Run();