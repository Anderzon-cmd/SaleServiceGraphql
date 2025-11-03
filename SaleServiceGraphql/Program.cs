using Microsoft.EntityFrameworkCore;
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


builder.Services
    .AddDbContextFactory<SaleContext>(opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    })
    .AddGraphQLServer()
    .AddAuthorization()
    .AddMutationConventions();

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
    opt.WithOrigins(["http://localhost:4000"]);
});


app.UseAuthentication();
app.UseAuthorization();



app.MapGraphQL();

app.RunWithGraphQLCommands(args);
