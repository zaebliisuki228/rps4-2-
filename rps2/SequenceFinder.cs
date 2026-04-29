//SequenceFinder.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathUtils
{
    // Структура, представляющая последовательность
    public struct Sequence
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Length => EndIndex - StartIndex + 1;
        public List<int> Values { get; set; }

        public override string ToString()
        {
            return "[" + string.Join(", ", Values) + "]";
        }
    }

    // Класс для поиска последовательностей
    public static class SequenceFinder
    {
        // Находит все неубывающие последовательности в массиве
        public static List<Sequence> FindAllSequences(int[] array)
        {
            List<Sequence> sequences = new List<Sequence>();

            if (array.Length == 0) return sequences;

            int start = 0;

            for (int i = 1; i < array.Length; i++)
            {
                // Если текущий элемент меньше предыдущего, последовательность прерывается
                if (array[i] < array[i - 1])
                {
                    sequences.Add(CreateSequence(array, start, i - 1));
                    start = i;
                }
            }
            // Сохраняем последнюю последовательность
            sequences.Add(CreateSequence(array, start, array.Length - 1));

            return sequences;
        }

        // Создаёт объект Sequence из части массива
        private static Sequence CreateSequence(int[] array, int start, int end)
        {
            Sequence seq = new Sequence();
            seq.StartIndex = start;
            seq.EndIndex = end;
            seq.Values = new List<int>();

            for (int i = start; i <= end; i++)
            {
                seq.Values.Add(array[i]);
            }

            return seq;
        }

        // Возвращает две последовательности с максимальной длиной
        public static List<Sequence> GetTwoLongest(List<Sequence> sequences)
        {
            if (sequences.Count == 0) return new List<Sequence>();

            // Сортируем по убыванию длины
            var sorted = sequences.OrderByDescending(s => s.Length).ToList();

            List<Sequence> result = new List<Sequence>();

            // Добавляем первую (самую длинную)
            result.Add(sorted[0]);

            // Ищем вторую, которая не пересекается с первой (желательно)
            for (int i = 1; i < sorted.Count; i++)
            {
                if (!IsOverlapping(result[0], sorted[i]))
                {
                    result.Add(sorted[i]);
                    break;
                }
            }

            // Если не нашли непересекающуюся, берём следующую по длине (даже если пересекается)
            if (result.Count < 2 && sorted.Count > 1)
            {
                result.Add(sorted[1]);
            }

            return result;
        }

        // Проверяет, пересекаются ли две последовательности
        private static bool IsOverlapping(Sequence seq1, Sequence seq2)
        {
            return !(seq1.EndIndex < seq2.StartIndex || seq2.EndIndex < seq1.StartIndex);
        }

        // Выводит результат на экран
        public static void PrintResult(int[] array, List<Sequence> twoLongest)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("Исходный массив: [" + string.Join(", ", array) + "]");
            Console.WriteLine($"Длина массива: {array.Length}");

            if (twoLongest.Count == 0)
            {
                Console.WriteLine("Последовательности не найдены.");
            }
            else if (twoLongest.Count == 1)
            {
                Console.WriteLine("Найдена только одна максимальная последовательность:");
                Console.WriteLine($"  Длина: {twoLongest[0].Length}, значения: {twoLongest[0]}");
            }
            else
            {
                Console.WriteLine("Две максимальные неубывающие последовательности:");
                for (int i = 0; i < twoLongest.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}) Длина: {twoLongest[i].Length}, значения: {twoLongest[i]}");
                    Console.WriteLine($"     Индексы: [{twoLongest[i].StartIndex}...{twoLongest[i].EndIndex}]");
                }
            }
            Console.WriteLine(new string('=', 50));
        }
    }
}