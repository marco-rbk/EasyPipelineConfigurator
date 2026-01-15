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
        sb.AppendLine($"name: {config.ProjectName} CI");
        sb.AppendLine();
        sb.AppendLine("on:");
        sb.AppendLine("  push:");
        sb.AppendLine("    branches: [ \"main\" ]");
        sb.AppendLine("  pull_request:");
        sb.AppendLine("    branches: [ \"main\" ]");
        sb.AppendLine();
        sb.AppendLine("jobs:");
        sb.AppendLine("  build:");
        sb.AppendLine("    runs-on: ubuntu-latest");
        sb.AppendLine();
        sb.AppendLine("    steps:");
        sb.AppendLine("    - uses: actions/checkout@v4");

        if (config.Framework == FrameworkType.DotNet)
        {
            sb.AppendLine("    - name: Setup .NET");
            sb.AppendLine("      uses: actions/setup-dotnet@v4");
            sb.AppendLine("      with:");
            sb.AppendLine("        dotnet-version: 8.0.x");
        }
        else if (config.Framework == FrameworkType.Python)
        {
            sb.AppendLine("    - name: Set up Python");
            sb.AppendLine("      uses: actions/setup-python@v4");
            sb.AppendLine("      with:");
            sb.AppendLine("        python-version: '3.x'");
        }
        else if (config.Framework == FrameworkType.Dart)
        {
            sb.AppendLine("    - name: Setup Dart");
            sb.AppendLine("      uses: dart-lang/setup-dart@v1");
        }

        if (config.BuildApplication && settings.Steps.ContainsKey("Restore"))
        {
            sb.AppendLine("    - name: Restore dependencies");
            sb.AppendLine($"      run: {settings.Steps["Restore"]}");
        }

        if (config.BuildApplication && settings.Steps.ContainsKey("Build"))
        {
            sb.AppendLine("    - name: Build");
            sb.AppendLine($"      run: {settings.Steps["Build"]}");
        }

        if (config.BuildApplication && settings.Steps.ContainsKey("Test"))
        {
            sb.AppendLine("    - name: Test");
            sb.AppendLine($"      run: {settings.Steps["Test"]}");
        }

        if (config.BuildRelease && settings.Steps.ContainsKey("Release"))
        {
            sb.AppendLine("    - name: Release");
            sb.AppendLine($"      run: {settings.Steps["Release"]}");
        }

        if (config.StartDeploy && settings.Steps.ContainsKey("Pack"))
        {
            sb.AppendLine("    - name: Pack");
            sb.AppendLine($"      run: {settings.Steps["Pack"]}");
        }

        if (config.StartDeploy && settings.Steps.ContainsKey("Push"))
        {
            sb.AppendLine("    - name: Push");
            sb.AppendLine($"      run: {settings.Steps["Push"]}");
        }

        return sb.ToString();
    }
}
