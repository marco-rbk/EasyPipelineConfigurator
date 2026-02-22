using EasyPipelineConfigurator.Core.Interfaces;

namespace EasyPipelineConfigurator.Infrastructure.Services;

/// <summary>
/// Writes files to the local filesystem, ensuring that directories exist.
/// </summary>
public class LocalFileWriteService : IFileWriteService
{
    /// <summary>
    /// Writes the specified content to a file at the given path asynchronously.
    /// Creates any missing directories in the path.
    /// </summary>
    /// <param name="path">Full path to the file to write.</param>
    /// <param name="content">File content.</param>
    public async Task WriteFileAsync(string path, string content)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(path, content);
    }
}
