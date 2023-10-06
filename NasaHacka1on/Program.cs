using BackendHackaton.Infrastracture.Extensions;
using Gybs;
using Gybs.Logic.Operations;
using Gybs.Logic.Validation;
using Microsoft.EntityFrameworkCore;
using NasaHacka1on.Database;
using NasaHacka1on.Infrastracture.Authentication;
using NasaHacka1on.Mail;
using NasaHacka1on.Models.Extensions;
using NasaHacka1on.Services;

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
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddGybs(builder =>
{
    builder.AddOperationInitializersForFactory();
    builder.AddServiceProviderOperationBus();
    builder.AddOperationFactory();
    builder.AddOperationHandlers();
    builder.AddValidation();
});

new NasaHacka1on.ModuleBootstrapper().Initialize(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Example API",
        Version = "v1",
        Description = "An example of an ASP.NET Core Web API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Example Contact",
            Email = "example@example.pl",
            Url = new Uri("https://example.com/contact")
        }
    });
});
builder.Services.AddCors();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
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
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:44418"));
app.UseAuthentication();

app.UseAuthorization();

app.UseCommunityCodeHubEndpoints();

//Production
//app.UseSpa(spa => spa.Options.DefaultPage = "/index.html");

app.Run();
