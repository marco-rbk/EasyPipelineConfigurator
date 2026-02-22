namespace EasyPipelineConfigurator.Core.Models;

/// <summary>
/// Contains named commands/steps for a framework used when generating pipelines.
/// Keys are logical step names (e.g., "Build", "Test", "Pack") and values are
/// the shell/command-line invocation to execute.
/// </summary>
public class FrameworkSettings
{
    /// <summary>
    /// Mapping of logical step names to command strings.
    /// </summary>
    public Dictionary<string, string> Steps { get; set; } = new();
}
