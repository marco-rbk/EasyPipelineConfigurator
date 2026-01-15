using System.Text;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Infrastructure.Generators;

public class GitLabPipelineGenerator : IPipelineGenerator
{
    private readonly ITemplateService _templateService;

    public GitLabPipelineGenerator(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    public PlatformType Type => PlatformType.GitLab;

    public string Generate(PipelineConfig config)
    {
        var settings = _templateService.GetSettings(config.Framework);
        var sb = new StringBuilder();

        GenerateHeader(sb, config);
        GenerateStages(sb, config);
        GenerateBuildJob(sb, config, settings);
        GenerateTestJob(sb, config, settings);
        GenerateReleaseJob(sb, config, settings);
        GenerateDeployJob(sb, config, settings);

        return sb.ToString();
    }

    private void GenerateHeader(StringBuilder sb, PipelineConfig config)
    {
        // Image selection based on framework
        string image = "ubuntu:latest";
        if (config.Framework == FrameworkType.DotNet) image = "mcr.microsoft.com/dotnet/sdk:8.0";
        else if (config.Framework == FrameworkType.Python) image = "python:3.11";
        else if (config.Framework == FrameworkType.Dart) image = "dart:stable";

        sb.AppendLine($"image: {image}");
        sb.AppendLine();
    }

    private void GenerateStages(StringBuilder sb, PipelineConfig config)
    {
        sb.AppendLine("stages:");
        if (config.BuildApplication) sb.AppendLine("  - build");
        if (config.BuildApplication) sb.AppendLine("  - test");
        if (config.BuildRelease) sb.AppendLine("  - release");
        if (config.StartDeploy) sb.AppendLine("  - deploy");
        sb.AppendLine();
    }

    private void GenerateBuildJob(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (!config.BuildApplication || !settings.Steps.ContainsKey("Build")) return;

        sb.AppendLine("build_job:");
        sb.AppendLine("  stage: build");
        sb.AppendLine("  script:");
        if (settings.Steps.ContainsKey("Restore")) sb.AppendLine($"    - {settings.Steps["Restore"]}");
        sb.AppendLine($"    - {settings.Steps["Build"]}");
        sb.AppendLine("  artifacts:");
        sb.AppendLine("    paths:");
        sb.AppendLine("      - bin/"); // Simplification
        sb.AppendLine();
    }

    private void GenerateTestJob(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (!config.BuildApplication || !settings.Steps.ContainsKey("Test")) return;

        sb.AppendLine("test_job:");
        sb.AppendLine("  stage: test");
        sb.AppendLine("  script:");
        sb.AppendLine($"    - {settings.Steps["Test"]}");
        sb.AppendLine();
    }

    private void GenerateReleaseJob(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (!config.BuildRelease || !settings.Steps.ContainsKey("Release")) return;

        sb.AppendLine("release_job:");
        sb.AppendLine("  stage: release");
        sb.AppendLine("  script:");
        sb.AppendLine($"    - {settings.Steps["Release"]}");
        sb.AppendLine("  artifacts:");
        sb.AppendLine("    paths:");
        sb.AppendLine("      - release_output/");
        sb.AppendLine();
    }

    private void GenerateDeployJob(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (!config.StartDeploy) return;

        sb.AppendLine("deploy_job:");
        sb.AppendLine("  stage: deploy");
        sb.AppendLine("  script:");
        if (settings.Steps.ContainsKey("Pack")) sb.AppendLine($"    - {settings.Steps["Pack"]}");
        if (settings.Steps.ContainsKey("Push")) sb.AppendLine($"    - {settings.Steps["Push"]}");
        sb.AppendLine("  only:");
        sb.AppendLine("    - main");
    }
}
