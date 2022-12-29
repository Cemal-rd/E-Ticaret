using API.Helpers;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Serilog.json", true).Build();
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                .CreateLogger();


// Add services to the container.
//iproductreponun scopedı buraya
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IOrder_DetailRepository,Order_DetailRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
builder.Services.AddAutoMapper(typeof(MappingProfiles));


builder.Services.AddControllers();
builder.Host.UseSerilog();
builder.Services.AddLogging();
builder.Services.AddDbContext<StoreContext>(options => 
{options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>{
        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200");
    });

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
try
{
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
   

}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
Log.Information("başarıyla çalıştı");
