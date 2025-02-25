using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Autofac.Core;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Utilities.Security.Encryption;
using Core.Utilities.IoC;
using Core.Extensions;
using Core.DependencyResolvers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
//Çünkü OnConfiguring metodunu zaten kullanýyoruz ve AddDbContext metoduna ihtiyacýmýz kalmýyor.
//NorthwindContext sýnýfý doðrudan baðlantý dizesine baðýmlý olur. appsettings.json gibi harici bir yapýlandýrma dosyasýndan okumaz.
//Baðlantý dizesi kod içinde sabit olduðu için güvenlik açýsýndan önerilmez.

//builder.Services.AddDbContext<NorthwindContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnection")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});


// duruma göre bu arkadaþý silebilrsin CoreModule.cs e taþýndý.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();  
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuration'dan TokenOptions'u çekiyoruz
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// JWT Authentication ekleniyor
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
//ServiceTool.Create(builder.Services);
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()

});


//Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject -->IoC Container
//kendi yazdýklarým

//Biz AOP yapacaðýz. 
//Postsharp
//builder.Services.AddSingleton<IProductService, ProductManager>();// services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Engin hocanýn yazdýklarý
//services.AddSingleton<IProductService, ProductManager>();