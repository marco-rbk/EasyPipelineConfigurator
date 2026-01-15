using System.Text.Json;
using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Core.Models;

namespace EasyPipelineConfigurator.Infrastructure.Services;

public class JsonTemplateService : ITemplateService
{
    private readonly string _basePath;

    public JsonTemplateService()
    {
        _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
    }

    public FrameworkSettings GetSettings(FrameworkType type)
    {
        var fileName = $"{type.ToString().ToLower()}.json";
        var path = Path.Combine(_basePath, fileName);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Template for {type} not found at {path}");
        }

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<FrameworkSettings>(json) ?? new FrameworkSettings();
    }
}
