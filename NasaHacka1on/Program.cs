using BackendHackaton.Infrastracture.Extensions;
using Gybs;
using Gybs.Logic.Operations;
using Gybs.Logic.Validation;
using Microsoft.EntityFrameworkCore;
using NasaHacka1on.Database;
using NasaHacka1on.Infrastracture.Authentication;
using NasaHacka1on.Models.Extensions;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;

builder.Services.AddDbContext<CommunityCodeHubSqlDataContext>(options =>
{
    options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:Database"),
        sql => sql.CommandTimeout(45));
});

builder.Services.AddScoped<CommunityCodeHubDataContext>(db => db.GetRequiredService<CommunityCodeHubSqlDataContext>());

builder.Services.AddBackendMvc(configuration);
builder.Services.AddBackendAuthentication(configuration);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IClaimsIdentityFactory, ClaimsIdentityFactory>();
builder.Services.AddTransient<IUserIdentityValidator, UserIdentityValidator>();

builder.Services.AddGybs(builder =>
{
    builder.AddOperationInitializersForFactory();
    builder.AddServiceProviderOperationBus();
    builder.AddOperationFactory();
    builder.AddOperationHandlers();
    builder.AddValidation();
});

new NasaHacka1on.ModuleBootstrapper().Initialize(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCommunityCodeHubEndpoints();

//Production
//app.UseSpa(spa => spa.Options.DefaultPage = "/index.html");

app.Run();
