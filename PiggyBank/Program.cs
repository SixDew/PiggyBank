using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyBank.Converters;
using PiggyBank.Database;
using PiggyBank.DTO;
using PiggyBank.Repositories;
using PiggyBank.Resources;
using PiggyBank.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.Services.AddScoped<IValidator<Transaction>, TransactionValidator>();
builder.Services.AddScoped<IValidator<Wallet>, WalletValidator>();
builder.Services.AddScoped<IValidator<WalletRenameDto>, WalletRenameValidator>();

builder.Services.AddTransient<IWalletConverter, WalletConverter>();
builder.Services.AddTransient<ITransactionConverter, TransactionConverter>();

builder.Services.AddDbContext<FinancesDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddScoped<IWalletRepository, WalletDbRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionDbRepository>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
