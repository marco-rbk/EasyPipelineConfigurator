using EasyPipelineConfigurator.Infrastructure.Generators;
using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Interfaces;
using Moq;

namespace EasyPipelineConfigurator.Tests;

public class GitLabPipelineGeneratorTests
{
    [Fact]
    public void Generate_ShouldReturnValidYaml_WhenConfigIsProvided()
    {
        // Arrange
        var mockTemplateService = new Mock<ITemplateService>();
        mockTemplateService.Setup(s => s.GetSettings(It.IsAny<FrameworkType>()))
            .Returns(new FrameworkSettings
            {
                Steps = new Dictionary<string, string>
                {
                    { "Build", "dotnet build" },
                    { "Release", "dotnet publish" },
                    { "Push", "dotnet nuget push" }
                }
            });

        var generator = new GitLabPipelineGenerator(mockTemplateService.Object);
        var config = new PipelineConfig
        {
            ProjectName = "TestProject",
            TargetPlatform = PlatformType.GitLab,
            Framework = FrameworkType.DotNet,
            BuildApplication = true,
            BuildRelease = true,
            StartDeploy = true,
            OutputDirectory = "./"
        };

        // Act
        var result = generator.Generate(config);

        // Assert
        Assert.Contains("image: mcr.microsoft.com/dotnet/sdk:8.0", result);
        Assert.Contains("stages:", result);
        Assert.Contains("- build", result);
        Assert.Contains("build_job:", result);
        Assert.Contains("dotnet build", result);
        Assert.Contains("release_job:", result);
        Assert.Contains("dotnet publish", result);
        Assert.Contains("deploy_job:", result);
        Assert.Contains("dotnet nuget push", result);
    }
}
