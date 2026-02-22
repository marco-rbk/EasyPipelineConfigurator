using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Models;

/// <summary>
/// Configuration options provided by the user that influence generated pipeline content.
/// </summary>
public class PipelineConfig
{
    /// <summary>
    /// The name of the project used in pipeline metadata.
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Target CI/CD platform to generate the pipeline for.
    /// </summary>
    public PlatformType TargetPlatform { get; set; }

    /// <summary>
    /// Selected framework/runtime type the project uses.
    /// </summary>
    public FrameworkType Framework { get; set; }

    /// <summary>
    /// Whether to include build application steps.
    /// </summary>
    public bool BuildApplication { get; set; }

    /// <summary>
    /// Whether to include a release build step.
    /// </summary>
    public bool BuildRelease { get; set; }

    /// <summary>
    /// Whether to include deployment steps (e.g., NuGet push).
    /// </summary>
    public bool StartDeploy { get; set; }

    /// <summary>
    /// Output directory where generated pipeline file will be written.
    /// </summary>
    public string OutputDirectory { get; set; } = "./";
}
