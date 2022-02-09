namespace FileAccessProgram;

public class FileAccess : IFileAccess
{
    private readonly DirectoryInfo _basePathInfo;
    public FileAccess(string basePath)
    {
        _basePathInfo = new DirectoryInfo(basePath);
        if (_basePathInfo.Exists == false)
        {
            throw new Exception("Invalid directory: " + basePath);
        }
    }

    public bool GetDirectory(string? path, out DirectoryInfo info)
    {
        string directoryPath = path == null ? _basePathInfo.FullName : Path.Combine(_basePathInfo.FullName, path);
        info = new DirectoryInfo(directoryPath);
        return info.Exists;
    }

    public DirectoryInfo BasePathInfo => _basePathInfo;
     
    public bool GetFile(string? fileName, out FileInfo info)
    {

        string path = fileName == null ? _basePathInfo.FullName : Path.Combine(_basePathInfo.FullName, fileName);
        info = new FileInfo(path);

        return info.Exists;
    }

    public FileInfo[] GetFiles(string? path = null)
    {
        if (GetDirectory(path, out DirectoryInfo info))
        {
            return info.GetFiles();
        }
        throw new Exception("invalid directory");
    }

    public DirectoryInfo[] GetDirectories(string? path = null)
    {
        if (GetDirectory(path, out DirectoryInfo info))
        {
            return info.GetDirectories();
        }
        throw new Exception("invalid directory");
    }
    
    public FileItem[] GetItems(string path=null)
    {
        IList<FileItem> list        = new List<FileItem>();
        DirectoryInfo[] directories = GetDirectories(path);
        FileInfo[] files            = GetFiles(path);
        
        foreach (var item in directories)
        {
            list.Add(FileItem.Create(item));
        }
        foreach (var item in files)
        {
            list.Add(FileItem.Create(item));
        }
        return list.ToArray();
    }

    public string ReadTextContent(string? path, string fileName)
    {
        if (GetDirectory(path, out DirectoryInfo info))
        {
            string fullPath = Path.Combine(info.FullName, fileName);
             
            if (!new FileInfo(fullPath).Exists)
            {
                throw new Exception($"file: {fullPath} not found");
            }
            return File.ReadAllText(fullPath);
        }
        throw new Exception("invalid file");
    }

    public bool RemoveFile(string? path, string fileName)
    {
        if (!GetDirectory(path, out DirectoryInfo directoryInfo))
        {
            throw new Exception("invalid path");
        }
        if (GetFile(Path.Combine(directoryInfo.FullName, fileName), out FileInfo fileInfo))
        {
            File.Delete(fileInfo.FullName);
            return true;
        }
        if (GetDirectory(Path.Combine(directoryInfo.FullName, fileName), out DirectoryInfo _))
        {
            Directory.Delete(Path.Combine(directoryInfo.FullName, fileName));
            return true;
        }
        
        throw new Exception("invalid file");
    }

    public bool WriteTextFile(string? path, string content, FileItem item)
    {
        if (!GetDirectory(path, out DirectoryInfo directoryInfo))
        {
            throw new Exception("Invalid path: " + path);
        }
        string fullName = Path.Combine(directoryInfo.FullName, item.Name);
        File.WriteAllText(fullName, content);
        return true;
    }

    public bool CreateFile(string? path, FileItem file)
    {
        if (!GetDirectory(path, out DirectoryInfo info))
        {
            throw new Exception("Invalid path: " + path);
        }
        string fullPath = Path.Combine(info.FullName, file.Name);

        switch (file.Attributes)
        {
            case FileAttributes.Directory:
                Directory.CreateDirectory(fullPath);
                break;
            case FileAttributes.Archive:
                File.Create(fullPath);
                break;
            default:
                throw new NotImplementedException();
        }
        
        return true;
    }
}