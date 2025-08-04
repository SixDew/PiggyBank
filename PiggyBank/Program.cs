using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyBank.Converters;
using PiggyBank.Converters.Interfaces;
using PiggyBank.Database;
using PiggyBank.DTO;
using PiggyBank.Repositories;
using PiggyBank.Repositories.Interfaces;
using PiggyBank.Services;
using PiggyBank.Services.Interfaces;
using PiggyBank.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

//Добавляем сериализатор TransactionDirection для возможности дальнейшей валидации
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new SafeEnumConverter<TransactionDirection>());
});

//Конвертеры данных
builder.Services.AddTransient<IWalletConverter, WalletConverter>();
builder.Services.AddTransient<ITransactionConverter, TransactionConverter>();

builder.Services.AddDbContext<FinancesDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

//Репозитории БД
builder.Services.AddScoped<IWalletRepository, WalletDbRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionDbRepository>();

//Валидаторы входных данных
builder.Services.AddScoped<IValidator<TransactionFromClientDto>, TransactionValidator>();
builder.Services.AddScoped<IValidator<WalletFromClientDto>, WalletValidator>();
builder.Services.AddScoped<IValidator<WalletRenameFromClientDto>, WalletRenameValidator>();

builder.Services.AddSingleton<IBalanceCounter, ThreadSafeBalanceCounter>();

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

    //Логируем чтобы отследить использование потоков для горячего пути
    app.Use(async (context, next) =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        ThreadPool.GetAvailableThreads(out int workerThreads, out int ioThreads);
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxIoThreads);

        logger.LogWarning($"Used threads: {maxWorkerThreads - workerThreads}");

        await next();
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
