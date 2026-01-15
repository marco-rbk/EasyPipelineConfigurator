using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Models;

public class PipelineConfig
{
    public string ProjectName { get; set; } = string.Empty;
    public PlatformType TargetPlatform { get; set; }
    public FrameworkType Framework { get; set; }
    public bool BuildApplication { get; set; }
    public bool BuildRelease { get; set; }
    public bool StartDeploy { get; set; }
    public string OutputDirectory { get; set; } = "./";
}
