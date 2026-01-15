using Microsoft.Extensions.DependencyInjection;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Infrastructure.Generators;
using EasyPipelineConfigurator.Infrastructure.Services;
using EasyPipelineConfigurator.Services;
using EasyPipelineConfigurator.Core.Models;

var serviceCollection = new ServiceCollection();

// Register Services
serviceCollection.AddTransient<IInputProvider, ConsoleInputProvider>();
serviceCollection.AddTransient<ITemplateService, JsonTemplateService>();
serviceCollection.AddTransient<IPipelineGenerator, GitHubPipelineGenerator>();
serviceCollection.AddTransient<IPipelineGenerator, GitLabPipelineGenerator>();
serviceCollection.AddTransient<IFileWriteService, LocalFileWriteService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Application Logic
var input = serviceProvider.GetRequiredService<IInputProvider>();
var generators = serviceProvider.GetServices<IPipelineGenerator>();
var fileService = serviceProvider.GetRequiredService<IFileWriteService>();

Console.WriteLine("Welcome to EasyPipelineConfigurator!");

var config = new PipelineConfig
{
    ProjectName = input.GetProjectName(),
    TargetPlatform = input.GetPlatformType(),
    Framework = input.GetFrameworkType(),
    BuildApplication = input.GetBuildApplication(),
    BuildRelease = input.GetBuildRelease(),
    StartDeploy = input.GetStartDeploy(),
    OutputDirectory = input.GetOutputDirectory()
};

var generator = generators.FirstOrDefault(g => g.Type == config.TargetPlatform);
if (generator == null)
{
    Console.WriteLine($"Error: No generator found for platform {config.TargetPlatform}");
    return;
}

var pipelineContent = generator.Generate(config);
var fileName = config.TargetPlatform == EasyPipelineConfigurator.Core.Enums.PlatformType.GitHub ? "pipeline.yml" : ".gitlab-ci.yml";
var outputPath = Path.Combine(config.OutputDirectory, fileName);

await fileService.WriteFileAsync(outputPath, pipelineContent);

Console.WriteLine($"Pipeline configuration generated at: {outputPath}");
Console.ReadKey();
