using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Interfaces;

public interface IPipelineGenerator
{
    PlatformType Type { get; }
    string Generate(PipelineConfig config);
}
