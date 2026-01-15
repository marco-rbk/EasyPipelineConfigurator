using EasyPipelineConfigurator.Core.Interfaces;

namespace EasyPipelineConfigurator.Infrastructure.Services;

public class LocalFileWriteService : IFileWriteService
{
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
