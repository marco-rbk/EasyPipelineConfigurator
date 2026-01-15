using EasyPipelineConfigurator.Infrastructure.Generators;
using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Tests;

public class GitHubPipelineGeneratorTests
{
    [Fact]
    public void Generate_ShouldReturnValidYaml_WhenConfigIsProvided()
    {
        // Arrange
        var generator = new GitHubPipelineGenerator();
        var config = new PipelineConfig
        {
            ProjectName = "TestProject",
            TargetPlatform = PlatformType.GitHub,
            BuildRelease = true,
            OutputDirectory = "./"
        };

        // Act
        var result = generator.Generate(config);

        // Assert
        Assert.Contains("name: TestProject CI", result);
        Assert.Contains("runs-on: ubuntu-latest", result);
        Assert.Contains("dotnet publish -c Release", result);
    }
}
