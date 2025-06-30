using Applications.Interface;
using Applications.Mapper;
using Applications.Service;
using Infrastructure.Contexts;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Applications.Middleware;
using Domain.Models;
using System.Text.Json.Serialization;
using Application.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Profilemapper));

// Database configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("InventoryAccountingSystem")));


builder.Services.AddScoped<IAuthRepo, AuthRepositery>();
builder.Services.AddScoped<Iauthservice, AuthService>();
builder.Services.AddScoped<ICatRepo, Catagoryrepo>();
builder.Services.AddScoped<ICatservice, CategoryService>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IproductService, ProductService>();
builder.Services.AddScoped<ICostmerRepo, CostomerRepo>();
builder.Services.AddScoped<ICostomerService, CostomerService>();
builder.Services.AddScoped<IVendorService,VendorService>();
builder.Services.AddScoped<IVentorrepo, VendorRepo>();
builder.Services.AddScoped<IStockRepo, StockRepo>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockTransactionServices, StockTransactionService>();
builder.Services.AddScoped<IStockTransactionsRepo, StockTransactionRepo>();
builder.Services.AddScoped<IpurchaseInvoiceRepo, PurchaseInvoiceRepo>();
builder.Services.AddScoped<Ipurchaseinvoiceservice, PurchaseInvoiceService>();
builder.Services.AddScoped<ISalesInvoiceRepo, SalesInvoiceREpo>();
builder.Services.AddScoped<ISalesInvoiceService, SalesInvoiceService>();
builder.Services.AddScoped<IIInvoicePdfGenerator, InvoicePdfGenerator>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILedgerRepo, LedgerREpo>();
builder.Services.AddScoped<ILedgerSErvice, LedgerSErvice>();
builder.Services.AddScoped<IJournalentryService, journalEntryService>();
builder.Services.AddScoped<IJournalEntrysRepo, JournalEntryRepo>();
builder.Services.AddScoped<IProfitlossRepo, ProfitAndLossRepo>();




var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = jwtSettings["Key"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = issuer,
//            ValidAudience = audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
//            ClockSkew = TimeSpan.Zero
//        };
//    });

// Swagger + JWT configuration
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your JWT token."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


})
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
        });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseMiddleware<Usermiddleware>();

app.MapControllers();

app.Run();
