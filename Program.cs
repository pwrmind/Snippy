using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Инициализация компонентов
        var dataKeeper = new DataKeeper();
        var linkFilter = new LinkFilter(new List<string> { "example.com" });
        var linkKeeper = new LinkKeeper(linkFilter);
        var linkProvider = new LinkProvider(linkKeeper);
        var dataProvider = new DataProvider(linkProvider);
        var linkExtractor = new LinkExtractor(linkKeeper);

        // Схема для DataExtractor
        var schema = new
        {
            schemaVersion = "V0",
            documentSchema = new[]
            {
                new { name = "title", cssSelector = "/html/body/div/h1" },
                new { name = "field", cssSelector = "/html/body/div/p" },
            }
        };
        var dataExtractor = new DataExtractor(dataKeeper, schema);

        // Начальная ссылка для сбора данных
        linkKeeper.AddLinks(new List<string> { "https://example.com" });

        // Основной цикл сбора данных
        for (int i = 0; i < 3; i++) // Ограничим количество итераций для примера
        {
            var data = await dataProvider.ProvideDataAsync();
            linkExtractor.Process(data);
            dataExtractor.Process(data);
        }

        // Вывод собранных данных
        Console.WriteLine("Сollected data:");
        foreach (var item in dataKeeper.Data)
        {
            foreach (var kvp in item)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}