using System.Text.Json;
using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Interfaces;
using EasyPipelineConfigurator.Core.Models;

namespace EasyPipelineConfigurator.Infrastructure.Services;

/// <summary>
/// Loads framework template settings from JSON files located in the Templates directory.
/// </summary>
public class JsonTemplateService : ITemplateService
{
    private readonly string _basePath;

    /// <summary>
    /// Initializes a new instance of <see cref="JsonTemplateService"/> and sets the base template directory.
    /// </summary>
    public JsonTemplateService()
    {
        _basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
    }

    /// <summary>
    /// Reads and deserializes the JSON template corresponding to the specified framework type.
    /// </summary>
    /// <param name="type">Framework type to load settings for.</param>
    /// <returns>A <see cref="FrameworkSettings"/> instance with loaded step definitions.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the template file does not exist.</exception>
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
