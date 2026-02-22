using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Interfaces;

/// <summary>
/// Represents a pipeline generator for a specific CI platform.
/// Implementations generate pipeline content (YAML) from a <see cref="PipelineConfig"/>.
/// </summary>
public interface IPipelineGenerator
{
    PlatformType Type { get; }
    string Generate(PipelineConfig config);
}
