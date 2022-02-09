namespace FileAccessProgram;

public class FileItem
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public FileAttributes Attributes { get; set; }

    public static FileItem Create(FileSystemInfo info)
    {   
        if (info.Exists == false)
        {
            throw new Exception($"File: {info.FullName} does not exist");
        }
        return new FileItem()
        {
            Name        = info.Name,
            Extension   = info.Extension,
            Attributes  = info.Attributes

        };
    }
    public static FileItem[] Create(FileSystemInfo[] infos)
    {
        IList<FileItem> items = new List<FileItem>();
        foreach (var item in infos)
        {
            items.Add(Create(item));
        }
        return items.ToArray();
    }

    internal static FileItem Archieve(string name)
    {
        return new FileItem
        {
            Name = name,
            Attributes = FileAttributes.Archive
        };
    }

    internal static FileItem Directory(string name)
    {
        return new FileItem
        {
            Name = name,
            Attributes = FileAttributes.Directory
        };
    }
}