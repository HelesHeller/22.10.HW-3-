using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("1 - Перегляд вмісту файлу");
        Console.WriteLine("2 - Збереження масиву у файл");
        Console.WriteLine("3 - Завантаження масиву з файлу");
        Console.WriteLine("4 - Розділення чисел на парні та непарні");
        Console.WriteLine("5 - Пошук слів у файлі");
        Console.WriteLine("6 - Статистика про файл");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                ViewFileContent();
                break;
            case 2:
                SaveArrayToFile();
                break;
            case 3:
                LoadArrayFromFile();
                break;
            case 4:
                SplitNumbers();
                break;
            case 5:
                SearchWordsInFile();
                break;
            case 6:
                FileStatistics();
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }

    static void ViewFileContent()
    {
        Console.WriteLine("Введіть шлях до файлу:");
        string path = Console.ReadLine();

        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            Console.WriteLine($"Вміст файлу:\n{content}");
        }
        else
        {
            Console.WriteLine("Файл не існує.");
        }
    }

    static void SaveArrayToFile()
    {
        Console.WriteLine("Введіть елементи масиву (розділіть пробілами):");
        string[] array = Console.ReadLine().Split(' ');

        Console.WriteLine("Введіть шлях до файлу для збереження:");
        string path = Console.ReadLine();

        File.WriteAllLines(path, array);
        Console.WriteLine("Масив успішно збережено у файл.");
    }

    static void LoadArrayFromFile()
    {
        Console.WriteLine("Введіть шлях до файлу для завантаження масиву:");
        string path = Console.ReadLine();

        if (File.Exists(path))
        {
            string[] array = File.ReadAllLines(path);
            Console.WriteLine($"Масив, завантажений з файлу: {string.Join(", ", array)}");
        }
        else
        {
            Console.WriteLine("Файл не існує.");
        }
    }

    static void SplitNumbers()
    {
        Random random = new Random();
        int[] numbers = Enumerable.Repeat(0, 10000)
                                 .Select(i => random.Next(1, 10001))
                                 .ToArray();

        string evenPath = "even_numbers.txt";
        string oddPath = "odd_numbers.txt";

        var evenNumbers = numbers.Where(num => num % 2 == 0);
        var oddNumbers = numbers.Where(num => num % 2 != 0);

        File.WriteAllLines(evenPath, evenNumbers.Select(num => num.ToString()));
        File.WriteAllLines(oddPath, oddNumbers.Select(num => num.ToString()));

        Console.WriteLine($"Парні числа збережено у файлі: {evenPath}");
        Console.WriteLine($"Непарні числа збережено у файлі: {oddPath}");
    }

    static void SearchWordsInFile()
    {
        Console.WriteLine("Введіть шлях до файлу:");
        string path = Console.ReadLine();

        Console.WriteLine("Введіть слово для пошуку:");
        string word = Console.ReadLine();

        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            bool containsWord = content.Contains(word);
            int wordCount = Regex.Matches(content, @"\b" + word + @"\b").Count;
            int reverseWordCount = Regex.Matches(content, @"\b" + new string(word.Reverse().ToArray()) + @"\b").Count;

            Console.WriteLine($"Слово '{word}' {(containsWord ? "знайдено" : "не знайдено")} в файлі.");
            Console.WriteLine($"Кількість входжень слова: {wordCount}");
            Console.WriteLine($"Кількість входжень слова у зворотному порядку: {reverseWordCount}");
        }
        else
        {
            Console.WriteLine("Файл не існує.");
        }
    }

    static void FileStatistics()
    {
        Console.WriteLine("Введіть шлях до файлу:");
        string path = Console.ReadLine();

        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            int sentenceCount = content.Split('.', '!', '?').Length - 1;
            int upperCaseCount = content.Count(char.IsUpper);
            int lowerCaseCount = content.Count(char.IsLower);
            int vowelCount = content.Count(c => "aeiouAEIOU".Contains(c));
            int consonantCount = content.Count(c => char.IsLetter(c) && !"aeiouAEIOU".Contains(c));
            int digitCount = content.Count(char.IsDigit);

            Console.WriteLine($"Кількість речень: {sentenceCount}");
            Console.WriteLine($"Кількість великих літер: {upperCaseCount}");
            Console.WriteLine($"Кількість маленьких літер: {lowerCaseCount}");
            Console.WriteLine($"Кількість голосних літер: {vowelCount}");
            Console.WriteLine($"Кількість приголосних літер: {consonantCount}");
            Console.WriteLine($"Кількість цифр: {digitCount}");
        }
        else
        {
            Console.WriteLine("Файл не існує.");
        }
    }
}
