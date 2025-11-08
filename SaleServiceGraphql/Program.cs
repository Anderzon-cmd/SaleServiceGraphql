using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using SaleServiceGraphql.Data;
using SaleServiceGraphql.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.MapInboundClaims = false;

    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});

// Configure MongoDB
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["MongoDB:DatabaseName"];
    return client.GetDatabase(databaseName);
});

builder.Services.AddScoped<SaleContext>();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddApolloFederation()
    .AddMutationConventions()
    .AddMongoDbFiltering()
    .AddMongoDbSorting()
    .AddMongoDbProjections();

builder.AddGraphQL()
    .AddTypes();

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<MarkService>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.WithOrigins(["http://localhost:4000","http://localhost:5173"]);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
