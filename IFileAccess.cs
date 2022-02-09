namespace FileAccessProgram
{

    public interface IFileAccess
    {
        DirectoryInfo BasePathInfo { get; }
        FileInfo[] GetFiles(string? path = null);
        DirectoryInfo[] GetDirectories(string? path = null);
        FileItem[] GetItems(string? path = null);
        bool GetDirectory(string? fileName, out DirectoryInfo info);
        bool GetFile(string fileName, out FileInfo info);
        string ReadTextContent(string? path, string fileName);
        bool CreateFile(string? path, FileItem file);
        bool WriteTextFile(string? fileName, string content, FileItem fileItem);
        bool RemoveFile(string? path, string fileName);




    }
}