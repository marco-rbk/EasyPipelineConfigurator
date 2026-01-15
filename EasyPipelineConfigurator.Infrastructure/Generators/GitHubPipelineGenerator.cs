using System.Text;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Infrastructure.Generators;

public class GitHubPipelineGenerator : IPipelineGenerator
{
    public PlatformType Type => PlatformType.GitHub;

    public string Generate(PipelineConfig config)
    {
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
        sb.AppendLine("    - name: Setup .NET");
        sb.AppendLine("      uses: actions/setup-dotnet@v4");
        sb.AppendLine("      with:");
        sb.AppendLine("        dotnet-version: 8.0.x");
        sb.AppendLine("    - name: Restore dependencies");
        sb.AppendLine("      run: dotnet restore");
        sb.AppendLine("    - name: Build");
        sb.AppendLine("      run: dotnet build --no-restore");
        sb.AppendLine("    - name: Test");
        sb.AppendLine("      run: dotnet test --no-build --verbosity normal");

        if (config.BuildRelease)
        {
            sb.AppendLine("    - name: Publish");
            sb.AppendLine("      run: dotnet publish -c Release -o release_output");
        }

        return sb.ToString();
    }
}
