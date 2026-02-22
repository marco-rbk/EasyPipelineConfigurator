using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Models;

namespace EasyPipelineConfigurator.Core.Interfaces;

/// <summary>
/// Service that retrieves framework-specific settings used to populate pipeline templates.
/// </summary>
public interface ITemplateService
{
    FrameworkSettings GetSettings(FrameworkType type);
}
