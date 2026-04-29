//Program.cs
using UserInterface;
using MathUtils;  
using System;
using System.Collections.Generic;

namespace LW2_Variant8;

class Program
{
    static void Main(string[] args)
    {
        const string info = """
        Смычкова Мария, гр. 444
        Лабораторная работя №2, вариант №8
        Задание: Найти в массиве две неубывающие последовательности максимальной длины
        """;

        Menu menu = new Menu(0, 7);
        int[] array = Array.Empty<int>();
        List<Sequence> twoLongest = new List<Sequence>();

        while (true)
        {
            Console.Write(info + "\n\n");
            switch (menu.PrintAndGetChoice())
            {
                case MenuItem.InputFromConsole:
                    while (true)
                    {
                        try
                        {
                            array = IOHandler.GetArrayFromConsole();
                            var allSequences = SequenceFinder.FindAllSequences(array);
                            twoLongest = SequenceFinder.GetTwoLongest(allSequences);
                            SequenceFinder.PrintResult(array, twoLongest);
                            menu.IsDataLoaded = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (!IOHandler.CheckForEnterKey()) continue;
                        break;
                    }
                    break;

                case MenuItem.LoadFromFile:
                    while (true)
                    {
                        try
                        {
                            string fileName = IOHandler.GetFileNameForInput();
                            array = IOHandler.ReadArrayFromFile(fileName);
                            var allSequences = SequenceFinder.FindAllSequences(array);
                            twoLongest = SequenceFinder.GetTwoLongest(allSequences);
                            SequenceFinder.PrintResult(array, twoLongest);
                            menu.IsDataLoaded = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (!IOHandler.CheckForEnterKey()) continue;
                        break;
                    }
                    break;

                case MenuItem.SaveInputToFile:
                    while (true)
                    {
                        try
                        {
                            string fileName = IOHandler.GetFileNameForOutput();
                            IOHandler.WriteArrayToFile(fileName, array);
                            IOHandler.PrintFullPath(fileName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (!IOHandler.CheckForEnterKey()) continue;
                        break;
                    }
                    break;

                case MenuItem.SaveResultToFile:
                    while (true)
                    {
                        try
                        {
                            string fileName = IOHandler.GetFileNameForOutput();
                            IOHandler.WriteResultToFile(fileName, array, twoLongest);
                            IOHandler.PrintFullPath(fileName);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (!IOHandler.CheckForEnterKey()) continue;
                        break;
                    }
                    break;

                case MenuItem.Exit:
                    return;
            }
            Console.Clear();
        }
    }
}