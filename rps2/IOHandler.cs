//IOHandler.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UserInterface;

public static class IOHandler
{
    // Ввод массива с консоли
    public static int[] GetArrayFromConsole()
    {
        Console.Write("Введите элементы массива через пробел: ");
        string input = GetLine();
        string[] parts = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        int[] array = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out array[i]))
                throw new Exception($"Некорректное число: {parts[i]}");
        }

        Console.WriteLine($"Введено {array.Length} элементов");
        return array;
    }

    // Чтение массива из файла
    public static int[] ReadArrayFromFile(string fileName)
    {
        string line = FileUtils.ReadLastLine(fileName);
        string[] parts = line.Split(new char[] { ' ', ',', '=', ':', '[', ']', ';' }, StringSplitOptions.RemoveEmptyEntries);

        List<int> numbers = new List<int>();
        foreach (string part in parts)
        {
            if (int.TryParse(part, out int num))
                numbers.Add(num);
        }

        if (numbers.Count == 0)
            throw new Exception("В файле не найдено чисел");

        Console.WriteLine($"Считано {numbers.Count} элементов из файла");
        return numbers.ToArray();
    }

    // Запись массива в файл
    public static void WriteArrayToFile(string fileName, int[] array)
    {
        File.AppendAllText(fileName, $"\nмассив: [{string.Join(", ", array)}]");
    }

    // Запись результата в файл
    public static void WriteResultToFile(string fileName, int[] array, List<MathUtils.Sequence> twoLongest)
    {
        string text = $"\nмассив: [{string.Join(", ", array)}]\nрезультат: ";
        if (twoLongest.Count == 0)
            text += "последовательности не найдены";
        else if (twoLongest.Count == 1)
            text += $"одна последовательность: {twoLongest[0]}";
        else
            text += $"две последовательности: {twoLongest[0]} и {twoLongest[1]}";

        File.AppendAllText(fileName, text);
    }

    // Спрашивает у пользователя файл, чтобы загрузить данные в программу
    public static string GetFileNameForInput()
    {
        string? fileName;
        Console.Write("Введите название файла для чтения: ");
        fileName = GetLine();

        // 1. Сначала ищем в текущей рабочей папке
        if (File.Exists(fileName))
        {
            Console.WriteLine($"Файл найден: {Path.GetFullPath(fileName)}");
            return fileName;
        }

        // 2. Ищем в папке, где находится EXE-файл
        string exePath = AppDomain.CurrentDomain.BaseDirectory;
        string fullPathExe = Path.Combine(exePath, fileName);
        if (File.Exists(fullPathExe))
        {
            Console.WriteLine($"Файл найден: {fullPathExe}");
            return fullPathExe;
        }

        // 3. Ищем в папке на уровень выше (bin\Debug -> проект)
        string parentPath = Path.GetFullPath(Path.Combine(exePath, "..", ".."));
        string fullPathParent = Path.Combine(parentPath, fileName);
        if (File.Exists(fullPathParent))
        {
            Console.WriteLine($"Файл найден: {fullPathParent}");
            return fullPathParent;
        }

        // 4. Ищем в папке с проектом (где лежит .csproj)
        try
        {
            string projectPath = Directory.GetParent(exePath).Parent.Parent.FullName;
            string fullPathProject = Path.Combine(projectPath, fileName);
            if (File.Exists(fullPathProject))
            {
                Console.WriteLine($"Файл найден: {fullPathProject}");
                return fullPathProject;
            }
        }
        catch (Exception) { /* Игнорируем ошибки получения пути */ }

        // 5. Ищем на рабочем столе
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fullPathDesktop = Path.Combine(desktopPath, fileName);
        if (File.Exists(fullPathDesktop))
        {
            Console.WriteLine($"Файл найден: {fullPathDesktop}");
            return fullPathDesktop;
        }

        // Если ничего не нашли — выдаём ошибку
        throw new Exception($"Файл \"{fileName}\" не найден.\n" +
            $"Проверенные папки:\n" +
            $"  1. Текущая папка: {Environment.CurrentDirectory}\n" +
            $"  2. Папка с программой: {exePath}\n" +
            $"  3. Папка проекта: {parentPath}\n" +
            $"  4. Рабочий стол: {desktopPath}");
    }

    // спрашивает у пользователя файл, чтобы загрузить в него данные
    public static string GetFileNameForOutput()
    {
        string? fileName;
        Console.Write("Введите название файла для сохранения: ");
        fileName = GetLine();

        if (!FileUtils.IsPathValid(fileName))
            throw new Exception("Путь к файлу содержит недопустимые символы");

        if (!FileUtils.IsFileNameValid(Path.GetFileName(fileName)))
            throw new Exception("Название файла содержит недопустимые символы");

        if (File.Exists(fileName))
        {
            if (YesOrNo("Указанный файл уже существует. Добавить данные в файл? (да/нет) "))
            {
                if (FileUtils.IsFileReadOnly(fileName))
                    throw new Exception("Указанный файл доступен только для чтения");
                if (FileUtils.IsFileDirectory(fileName))
                    throw new Exception("По указанному пути находится папка, а не файл");
                return fileName;
            }
            else
            {
                throw new Exception("Операция отменена пользователем");
            }
        }
        else
        {
            return fileName;
        }
    }

    // выводит полный путь к файлу в консоль
    public static void PrintFullPath(string fileName)
    {
        Console.WriteLine("Файл был сохранен по адресу: " + Path.GetFullPath(fileName));
    }

    // считывает непустую строку с консоли
    private static string GetLine()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (input != "" && input != null) return input;
        }
    }

    // спрашивает пользователя, возвращает true или false, в зависимости от ответа
    private static bool YesOrNo(string message)
    {
        string? response;
        while (true)
        {
            Console.Write(message);
            response = GetLine();
            if (response.ToLower() == "да") return true;
            if (response.ToLower() == "нет") return false;
            Console.WriteLine("Некорректный ответ");
        }
    }

    // возвращает true, если была нажата клавиша Enter, в противном случае возвращает false
    public static bool CheckForEnterKey(string message = "Нажмите Enter для выхода в меню или любую другую клавишу, чтобы повторить ввод.")
    {
        Console.WriteLine(message);
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        return keyInfo.Key == ConsoleKey.Enter;
    }
}