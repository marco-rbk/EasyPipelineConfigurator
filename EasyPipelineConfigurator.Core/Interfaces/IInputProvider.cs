using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Interfaces;

public interface IInputProvider
{
    PlatformType GetPlatformType();
    FrameworkType GetFrameworkType();
    string GetProjectName();
    bool GetBuildApplication();
    bool GetBuildRelease();
    bool GetStartDeploy();
    string GetOutputDirectory();
}
