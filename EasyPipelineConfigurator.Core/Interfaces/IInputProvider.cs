using EasyPipelineConfigurator.Core.Models;
using EasyPipelineConfigurator.Core.Enums;

namespace EasyPipelineConfigurator.Core.Interfaces;

/// <summary>
/// Abstraction for a provider that supplies user input required to configure pipeline generation.
/// Implementations can provide interactive console input, file-based input or other sources.
/// </summary>
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
