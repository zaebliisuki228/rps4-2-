//FileUtils.cs
using System;
using System.IO;

namespace UserInterface;

public static class FileUtils
{
    // считывает последнюю строку файла
    public static string ReadLastLine(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        if (lines.Length == 0)
            throw new Exception("Файл не содержит ни одной строки");
        return lines[lines.Length - 1];
    }

    // проверяет содержит ли путь к файлу некорректные символы
    public static bool IsPathValid(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return false;
        }

        char[] invalidChars = Path.GetInvalidPathChars();

        if (path.IndexOfAny(invalidChars) != -1)
        {
            return false;
        }

        return true;
    }

    // проверяет содержит ли название файла некорректные символы
    public static bool IsFileNameValid(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return false;
        }

        char[] invalidChars = Path.GetInvalidFileNameChars();

        if (fileName.IndexOfAny(invalidChars) != -1)
        {
            return false;
        }

        return true;
    }

    // проверяет не является ли указанный файл папкой
    public static bool IsFileDirectory(string fileName)
    {
        return (File.GetAttributes(fileName) & FileAttributes.Directory) == FileAttributes.Directory;
    }

    // проверяет доступен ли файл только для чтения
    public static bool IsFileReadOnly(string fileName)
    {
        if (!File.Exists(fileName)) return false;
        return (File.GetAttributes(fileName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
    }
}