namespace EasyPipelineConfigurator.Core.Interfaces;

public interface IFileWriteService
{
    Task WriteFileAsync(string path, string content);
}
