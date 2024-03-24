using AutoMapper;
using BankAccount;
using BankAccount.AutoMapperProfiles;
using BankAccount.Middlewares;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(opt => opt.SerializerSettings.Converters.Add(new StringEnumConverter()));
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSwaggerGenNewtonsoftSupport();

var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(new List<Profile>
{
    new AccountDtoPostgresProfile(),
    new ClientDtoPostgresProfile(),
    new TransactionDtoPostgresProfile()
}));
builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.ConfigureDatabaseConnection(builder.Configuration);
builder.Services.AddLogging(opt => opt.AddConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseRequestResponseLogging();
}

app.UseMiddleware<ExceptionHandleMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
