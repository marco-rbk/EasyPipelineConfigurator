using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Models;

namespace EasyPipelineConfigurator.Core.Interfaces;

public interface ITemplateService
{
    FrameworkSettings GetSettings(FrameworkType type);
}
