using AutoMapper;
using Desafio_UOL.Data;
using Desafio_UOL.Models;
using Desafio_UOL.Repository;
using Desafio_UOL.Services;
using Desafio_UOL.ViewModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region DI
builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<ICodinomeRepository, CodinomeRepository>();
builder.Services.AddScoped<ICodinomeService, CodinomeService>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
builder.Services.AddScoped<IGrupoService, GrupoService>();
builder.Services.AddScoped<IXMLService, XMLService>();
builder.Services.AddScoped<IJSONService, JSONService>();
builder.Services.AddScoped<HttpClient, HttpClient>();
#endregion


#region DB
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

// Registra o DbContext como um serviço singleton no DI
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region AUTOMAPPER
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;
    c.CreateMap<JogadorModel, JogadorCadastroViewModel>();
    c.CreateMap<JogadorCadastroViewModel, JogadorModel>();
    c.CreateMap<JogadorModel, JogadorUpdateViewModel>();
    c.CreateMap<JogadorModel, JogadorExibicaoViewModel>();
    c.CreateMap<JogadorUpdateViewModel, JogadorModel>();
    c.CreateMap<JogadorModel, JogadorUpdateViewModel>();
    c.CreateMap<JogadorExibicaoViewModel, JogadorModel>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
