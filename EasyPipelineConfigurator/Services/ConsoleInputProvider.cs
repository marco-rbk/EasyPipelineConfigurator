using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Interfaces;

namespace EasyPipelineConfigurator.Services;

/// <summary>
/// Provides console-based user input for pipeline configuration options.
/// Implements <see cref="IInputProvider"/> to supply platform, framework and generation choices.
/// </summary>
public class ConsoleInputProvider : IInputProvider
{
    public PlatformType GetPlatformType()
    {
        Console.WriteLine("Select Platform:");
        Console.WriteLine("1. GitHub");
        Console.WriteLine("2. GitLab");

        while (true)
        {
            Console.Write("Choice (1-2): ");
            var key = Console.ReadLine();
            switch (key)
            {
                case "1": return PlatformType.GitHub;
                case "2": return PlatformType.GitLab;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Prompts the user to select a target framework type and returns the chosen value.
    /// </summary>
    /// <returns>The selected <see cref="FrameworkType"/>.</returns>
    public FrameworkType GetFrameworkType()
    {
        Console.WriteLine("Select Framework:");
        Console.WriteLine("1. .NET");
        Console.WriteLine("2. Dart");
        Console.WriteLine("3. Python");

        while (true)
        {
            Console.Write("Choice (1-3): ");
            var key = Console.ReadLine();
            switch (key)
            {
                case "1": return FrameworkType.DotNet;
                case "2": return FrameworkType.Dart;
                case "3": return FrameworkType.Python;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Prompts the user to enter a project name.
    /// </summary>
    /// <returns>The entered project name or a default if none provided.</returns>
    public string GetProjectName()
    {
        Console.Write("Enter Project Name: ");
        return Console.ReadLine() ?? "MyProject";
    }

    /// <summary>
    /// Asks whether to include build application steps in the generated pipeline.
    /// </summary>
    /// <returns>True if the build application step should be included; otherwise false.</returns>
    public bool GetBuildApplication()
    {
        Console.Write("Include Build Application step? (y/n): ");
        var key = Console.ReadLine();
        return key?.ToLower() == "y";
    }

    /// <summary>
    /// Asks whether to include a release build step in the generated pipeline.
    /// </summary>
    /// <returns>True if the release build step should be included; otherwise false.</returns>
    public bool GetBuildRelease()
    {
        Console.Write("Include Release Build step? (y/n): ");
        var key = Console.ReadLine();
        return key?.ToLower() == "y";
    }

    /// <summary>
    /// Asks whether to include deploy (NuGet push) steps in the generated pipeline.
    /// </summary>
    /// <returns>True if deploy steps should be included; otherwise false.</returns>
    public bool GetStartDeploy()
    {
        Console.Write("Include Deploy (NuGet Push) step? (y/n): ");
        var key = Console.ReadLine();
        return key?.ToLower() == "y";
    }

    /// <summary>
    /// Prompts the user for an output directory where the generated pipeline file will be saved.
    /// </summary>
    /// <returns>The output directory path or the current directory if none provided.</returns>
    public string GetOutputDirectory()
    {
        Console.Write("Enter Output Directory (default: current): ");
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? "./" : input;
    }
}
