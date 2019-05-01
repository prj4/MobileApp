namespace Photobook.Models
{
    public interface IFileDirectoryAPI
    {
        string GetImagePath();
        string GetTempPath();
    }
}