using AutoMapper;
using BankAccount;
using BankAccount.AutoMapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc => mc.AddProfiles(new List<Profile>
{
    new AccountDtoPostgresProfile(),
    new ClientDtoPostgresProfile(),
    new TransactionDtoPostgresProfile()
}));
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.ConfigureDatabaseConnection(builder.Configuration);
builder.Services.AddLogging(opt => opt.AddConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRequestResponseLogging();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
