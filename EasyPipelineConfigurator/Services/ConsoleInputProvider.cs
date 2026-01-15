using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Interfaces;

namespace EasyPipelineConfigurator.Services;

public class ConsoleInputProvider : IInputProvider
{
    public PlatformType GetPlatformType()
    {
        Console.WriteLine("Select Platform:");
        Console.WriteLine("1. GitHub");
        Console.WriteLine("2. GitLab");
        Console.WriteLine("3. Azure DevOps");

        while (true)
        {
            Console.Write("Choice (1-3): ");
            var key = Console.ReadLine();
            switch (key)
            {
                case "1": return PlatformType.GitHub;
                case "2": return PlatformType.GitLab;
                case "3": return PlatformType.AzureDevOps;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    public string GetProjectName()
    {
        Console.Write("Enter Project Name: ");
        return Console.ReadLine() ?? "MyProject";
    }

    public bool GetBuildRelease()
    {
        Console.Write("Include Release Build step? (y/n): ");
        var key = Console.ReadLine();
        return key?.ToLower() == "y";
    }

    public string GetOutputDirectory()
    {
        Console.Write("Enter Output Directory (default: current): ");
        var input = Console.ReadLine();
        return string.IsNullOrWhiteSpace(input) ? "./" : input;
    }
}
