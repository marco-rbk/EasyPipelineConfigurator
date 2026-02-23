using System;
using System;
using System.Collections.Generic;
using EasyPipelineConfigurator.Core.Enums;
using EasyPipelineConfigurator.Core.Interfaces;

namespace EasyPipelineConfigurator.Services;

/// <summary>
/// Provides console-based user input for pipeline configuration options.
/// Implements <see cref="IInputProvider"/> to supply platform, framework and generation choices.
/// </summary>
public class ConsoleInputProvider : IInputProvider
{
    // Renders a simple interactive menu that supports Up/Down arrows, Enter and number keys.
    private T SelectFromOptions<T>(string title, List<(string Label, T Value)> options)
    {
        if (options == null || options.Count == 0)
            throw new ArgumentException("Options must contain at least one element", nameof(options));

        int index = 0;
        ConsoleKey key;
        do
        {
            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine("(Use Up/Down or press number, then Enter)");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == index)
                {
                    var prevBg = Console.BackgroundColor;
                    var prevFg = Console.ForegroundColor;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {i + 1}. {options[i].Label}");
                    Console.BackgroundColor = prevBg;
                    Console.ForegroundColor = prevFg;
                }
                else
                {
                    Console.WriteLine($"  {i + 1}. {options[i].Label}");
                }
            }

            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow)
            {
                index = (index - 1 + options.Count) % options.Count;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                index = (index + 1) % options.Count;
            }
            else if (key >= ConsoleKey.D1 && key <= ConsoleKey.D9)
            {
                int num = key - ConsoleKey.D0;
                if (num >= 1 && num <= options.Count)
                    return options[num - 1].Value;
            }
            else if (key >= ConsoleKey.NumPad1 && key <= ConsoleKey.NumPad9)
            {
                int num = key - ConsoleKey.NumPad0;
                if (num >= 1 && num <= options.Count)
                    return options[num - 1].Value;
            }
            else if (key == ConsoleKey.Enter)
            {
                return options[index].Value;
            }

            // Redraw: move cursor up to rewrite the menu
            int linesToMoveUp = options.Count + 3; // title + instruction + options
            for (int i = 0; i < linesToMoveUp; i++)
                Console.SetCursorPosition(0, Math.Max(Console.CursorTop - 1, 0));

        } while (true);
    }

    private bool Confirm(string prompt, bool defaultYes = false)
    {
        var options = new List<(string, bool)>
        {
            ("Yes", true),
            ("No", false)
        };

        return SelectFromOptions(prompt, options);
    }

    public PlatformType GetPlatformType()
    {
        var options = new List<(string, PlatformType)>
        {
            ("GitHub", PlatformType.GitHub),
            ("GitLab", PlatformType.GitLab)
        };

        return SelectFromOptions("Select Platform:", options);
    }

    /// <summary>
    /// Prompts the user to select a target framework type and returns the chosen value.
    /// </summary>
    /// <returns>The selected <see cref="FrameworkType"/>.</returns>
    public FrameworkType GetFrameworkType()
    {
        var options = new List<(string, FrameworkType)>
        {
            (".NET", FrameworkType.DotNet),
            ("Dart", FrameworkType.Dart),
            ("Python", FrameworkType.Python)
        };

        return SelectFromOptions("Select Framework:", options);
    }

    /// <summary>
    /// Prompts the user to enter a project name.
    /// </summary>
    /// <returns>The entered project name or a default if none provided.</returns>
    public string GetProjectName()
    {
        Console.Write("Enter Project Name (default: MyProject): ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
            return "MyProject";

        return input;
    }

    /// <summary>
    /// Asks whether to include build application steps in the generated pipeline.
    /// </summary>
    /// <returns>True if the build application step should be included; otherwise false.</returns>
    public bool GetBuildApplication()
    {
        return Confirm("Include Build Application step?");
    }

    /// <summary>
    /// Asks whether to include a release build step in the generated pipeline.
    /// </summary>
    /// <returns>True if the release build step should be included; otherwise false.</returns>
    public bool GetBuildRelease()
    {
        return Confirm("Include Release Build step?");
    }

    /// <summary>
    /// Asks whether to include deploy (NuGet push) steps in the generated pipeline.
    /// </summary>
    /// <returns>True if deploy steps should be included; otherwise false.</returns>
    public bool GetStartDeploy()
    {
        return Confirm("Include Deploy (NuGet Push) step?");
    }

    /// <summary>
    /// Prompts the user for an output directory where the generated pipeline file will be saved.
    /// </summary>
    /// <returns>The output directory path or the current directory if none provided.</returns>
    public string GetOutputDirectory()
    {
        Console.Write("Enter Output Directory (default: current): ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
            return "./";

        return input;
    }
}
