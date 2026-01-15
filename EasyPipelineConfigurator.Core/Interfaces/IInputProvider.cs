using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Interfaces;

public interface IInputProvider
{
    PlatformType GetPlatformType();
    string GetProjectName();
    bool GetBuildRelease();
    string GetOutputDirectory();
}
