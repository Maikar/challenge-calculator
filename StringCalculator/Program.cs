using Microsoft.Extensions.DependencyInjection;
using StringCalculator.Managers;
using StringCalculator.Models;

var services = new ServiceCollection();
services.AddLogging();
services.AddScoped<IOperationManager, OperationManager>();

var provider = services.BuildServiceProvider();

var calculationManager = provider.GetRequiredService<IOperationManager>();

Console.Write("Input: ");

var input = Console.ReadLine();

var response = calculationManager.Add(new AddRequest(input));

Console.WriteLine($"Result: {response.Result}");
Console.WriteLine($"Formula: {response.Formula}");