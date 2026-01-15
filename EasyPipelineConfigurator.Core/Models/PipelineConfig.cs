using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Models;

public class PipelineConfig
{
    public string ProjectName { get; set; } = string.Empty;
    public PlatformType TargetPlatform { get; set; }
    public bool BuildRelease { get; set; }
    public string OutputDirectory { get; set; } = "./";
}
