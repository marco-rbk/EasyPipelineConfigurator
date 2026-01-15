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

    public string GetProjectName()
    {
        Console.Write("Enter Project Name: ");
        return Console.ReadLine() ?? "MyProject";
    }

    public bool GetBuildApplication()
    {
        Console.Write("Include Build Application step? (y/n): ");
        var key = Console.ReadLine();
        return key?.ToLower() == "y";
    }

    public bool GetBuildRelease()
    {
        Console.Write("Include Release Build step? (y/n): ");
        var key = Console.ReadLine();
        return key?.ToLower() == "y";
    }

    public bool GetStartDeploy()
    {
        Console.Write("Include Deploy (NuGet Push) step? (y/n): ");
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
