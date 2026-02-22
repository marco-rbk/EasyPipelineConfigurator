namespace EasyPipelineConfigurator.Core.Interfaces;

/// <summary>
/// Abstraction for writing files to an output target. Allows for different implementations
/// such as local file system or remote storage.
/// </summary>
public interface IFileWriteService
{
    Task WriteFileAsync(string path, string content);
}
