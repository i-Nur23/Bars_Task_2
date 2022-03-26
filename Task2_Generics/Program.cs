using System;
using System.IO;

namespace Program;
internal static class Program
{
    private static void Main()
    {
        string path = @"..\..\..\CheckingWork.txt";

        LocalFileLogger<int> localFileLoggerInt = new LocalFileLogger<int>(path);
        LocalFileLogger<string> localFileLoggerStr = new LocalFileLogger<string>(path);
        LocalFileLogger<MyClass> localFileLoggerClass = new LocalFileLogger<MyClass>(path);

        localFileLoggerInt.LogInfo("Info_Int"); localFileLoggerInt.LogWarning("Warning_Int"); localFileLoggerInt.LogError("Error_Int", new Exception());
        localFileLoggerStr.LogInfo("Info_Str"); localFileLoggerStr.LogWarning("Warning_Str"); localFileLoggerStr.LogError("Error_Str", new NullReferenceException());
        localFileLoggerClass.LogInfo("Info_My"); localFileLoggerClass.LogWarning("Warning_Class"); localFileLoggerClass.LogError("Error in MyClass", new DivideByZeroException());
    }
}

interface ILogger
{
    public void LogInfo(string message);

    public void LogWarning(string message);

    public void LogError(string message, Exception ex);
}

public class LocalFileLogger<T> : ILogger
{
    private string path;

    private string GenericTypeName = typeof(T).Name;

    public LocalFileLogger(string path)
    {
        this.path = path;
        File.WriteAllText(path, "");
    }

    public void LogInfo(string message)
    {
        File.AppendAllText(path, $"[Info]: [{GenericTypeName}] : {message}\n");
    }

    public void LogWarning(string message)
    {
        File.AppendAllText(path, $"[Warning] : [{GenericTypeName}] : {message}\n");
    }

    public void LogError(string message, Exception ex)
    {
        File.AppendAllText(path, $"[Error] : [{GenericTypeName}] : {message}. {ex.Message}\n");
    }
}

public class MyClass
{
    private int number;
    public MyClass()
    {
        number = 0;
    }

    public void Print()
    {
        Console.WriteLine(number);
    }
}