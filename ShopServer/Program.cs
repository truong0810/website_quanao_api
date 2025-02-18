using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ShopServer.Business.Implement;
using ShopServer.Business.Inteface;
using ShopServer.Business.Ultis.Jwt;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5258);
});

// Add services to the container.
builder.Services.AddScoped<IAuthBusiness, AuthBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IBlogBusiness, BlogBusiness>();
builder.Services.AddScoped<IBlogCategoryBusiness, BlogCategoryBusiness>();
builder.Services.AddScoped<IBrandBusiness, BrandBusiness>();
builder.Services.AddScoped<ICachingBusiness, CachingBusiness>();
builder.Services.AddScoped<ITagBusiness, TagBusiness>();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
builder.Services.AddScoped<IBrandBusiness, BrandBusiness>();
builder.Services.AddScoped<ICategoryBusiness, CategoryBusiness>();
builder.Services.AddScoped<IPromotionBusiness, PromotionBusiness>();
builder.Services.AddScoped<IProductPriceBusiness, ProductPriceBusiness>();
builder.Services.AddScoped<IResourceBusiness, ResourceBusiness>();
builder.Services.AddScoped<IPaymentBusiness, PaymentBusiness>();
builder.Services.AddScoped<IPartnerBusiness, PartnerBusiness>();
builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
builder.Services.AddScoped<IBannerBusiness, BannerBusiness>();

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
//?? ý cá này , t? net6 tr? lên n?u không các thu?c tính tham chi?u không nullable là b?t bu?c
//([Required]) khi th?c hi?n xác th?c mô hình (model validation). 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("AllowSpecificOrigin", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy => policy.RequireClaim("UserId"));

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["token-valid-issuer"],
                        ValidAudience = configuration["token-valid-audience"],
                        IssuerSigningKey = JwtSecurityKey.Create(configuration["secret-key-name"])
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
