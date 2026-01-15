using Microsoft.Extensions.DependencyInjection;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Infrastructure.Generators;
using EasyPipelineConfigurator.Infrastructure.Services;
using EasyPipelineConfigurator.Services;
using EasyPipelineConfigurator.Core.Models;

var serviceCollection = new ServiceCollection();

// Register Services
serviceCollection.AddTransient<IInputProvider, ConsoleInputProvider>();
serviceCollection.AddTransient<IPipelineGenerator, GitHubPipelineGenerator>(); // Currently only supporting GitHub for simplicity of V1
serviceCollection.AddTransient<IFileWriteService, LocalFileWriteService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Application Logic
var input = serviceProvider.GetRequiredService<IInputProvider>();
var generator = serviceProvider.GetRequiredService<IPipelineGenerator>();
var fileService = serviceProvider.GetRequiredService<IFileWriteService>();

Console.WriteLine("Welcome to EasyPipelineConfigurator!");

var config = new PipelineConfig
{
    ProjectName = input.GetProjectName(),
    TargetPlatform = input.GetPlatformType(),
    BuildRelease = input.GetBuildRelease(),
    OutputDirectory = input.GetOutputDirectory()
};

var pipelineContent = generator.Generate(config);
var outputPath = Path.Combine(config.OutputDirectory, "pipeline.yml"); // Simplification for file name

await fileService.WriteFileAsync(outputPath, pipelineContent);

Console.WriteLine($"Pipeline configuration generated at: {outputPath}");
Console.ReadKey();
