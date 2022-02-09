using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileAccessProgram;

public class Program
{
    const string BasePath = "D:\\Development\\";

    private static JsonSerializerOptions _options = new JsonSerializerOptions(){
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

    
    public static void Main(string[] args)
    {
        _options.Converters.Add(new JsonStringEnumConverter());

        Console.WriteLine(Environment.CurrentDirectory);
        Console.WriteLine($"Hello world\r\n");

        IFileAccess access = new FileAccess(BasePath);
        FileItem[] infos   = access.GetItems("DotNet");
        Console.WriteLine(ToJson(infos));

    }

    private static string ToJson(object obj)
    {
        return JsonSerializer.Serialize( obj, _options)  ; 
    }
}
