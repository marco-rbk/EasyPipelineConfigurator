using System.Text;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Infrastructure.Generators;

public class GitHubPipelineGenerator : IPipelineGenerator
{
    private readonly ITemplateService _templateService;

    public GitHubPipelineGenerator(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    public PlatformType Type => PlatformType.GitHub;

    public string Generate(PipelineConfig config)
    {
        var settings = _templateService.GetSettings(config.Framework);
        var sb = new StringBuilder();

        GenerateHeader(sb, config);
        GenerateTriggers(sb);
        GenerateJobsHeader(sb);
        GenerateSetupSteps(sb, config);
        GenerateBuildSteps(sb, config, settings);
        GenerateReleaseSteps(sb, config, settings);
        GenerateDeploySteps(sb, config, settings);

        return sb.ToString();
    }

    private void GenerateHeader(StringBuilder sb, PipelineConfig config)
    {
        sb.AppendLine($"name: {config.ProjectName} CI");
        sb.AppendLine();
    }

    private void GenerateTriggers(StringBuilder sb)
    {
        sb.AppendLine("on:");
        sb.AppendLine("  push:");
        sb.AppendLine("    branches: [ \"main\" ]");
        sb.AppendLine("  pull_request:");
        sb.AppendLine("    branches: [ \"main\" ]");
        sb.AppendLine();
    }

    private void GenerateJobsHeader(StringBuilder sb)
    {
        sb.AppendLine("jobs:");
        sb.AppendLine("  build:");
        sb.AppendLine("    runs-on: ubuntu-latest");
        sb.AppendLine();
        sb.AppendLine("    steps:");
        sb.AppendLine("    - uses: actions/checkout@v4");
    }

    private void GenerateSetupSteps(StringBuilder sb, PipelineConfig config)
    {
        switch (config.Framework)
        {
            case FrameworkType.DotNet:
                sb.AppendLine("    - name: Setup .NET");
                sb.AppendLine("      uses: actions/setup-dotnet@v4");
                sb.AppendLine("      with:");
                sb.AppendLine("        dotnet-version: 8.0.x");
                break;
            case FrameworkType.Python:
                sb.AppendLine("    - name: Set up Python");
                sb.AppendLine("      uses: actions/setup-python@v4");
                sb.AppendLine("      with:");
                sb.AppendLine("        python-version: '3.x'");
                break;
            case FrameworkType.Dart:
                sb.AppendLine("    - name: Setup Dart");
                sb.AppendLine("      uses: dart-lang/setup-dart@v1");
                break;
        }
    }

    private void GenerateBuildSteps(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (!config.BuildApplication) return;

        if (settings.Steps.ContainsKey("Restore"))
        {
            sb.AppendLine("    - name: Restore dependencies");
            sb.AppendLine($"      run: {settings.Steps["Restore"]}");
        }

        if (settings.Steps.ContainsKey("Build"))
        {
            sb.AppendLine("    - name: Build");
            sb.AppendLine($"      run: {settings.Steps["Build"]}");
        }

        if (settings.Steps.ContainsKey("Test"))
        {
            sb.AppendLine("    - name: Test");
            sb.AppendLine($"      run: {settings.Steps["Test"]}");
        }
    }

    private void GenerateReleaseSteps(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (config.BuildRelease && settings.Steps.ContainsKey("Release"))
        {
            sb.AppendLine("    - name: Release");
            sb.AppendLine($"      run: {settings.Steps["Release"]}");
        }
    }

    private void GenerateDeploySteps(StringBuilder sb, PipelineConfig config, FrameworkSettings settings)
    {
        if (!config.StartDeploy) return;

        if (settings.Steps.ContainsKey("Pack"))
        {
            sb.AppendLine("    - name: Pack");
            sb.AppendLine($"      run: {settings.Steps["Pack"]}");
        }

        if (settings.Steps.ContainsKey("Push"))
        {
            sb.AppendLine("    - name: Push");
            sb.AppendLine($"      run: {settings.Steps["Push"]}");
        }
    }
}
