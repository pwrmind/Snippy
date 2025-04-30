using HtmlAgilityPack;
using System;
using System.Collections.Generic;

public class DataExtractor
{
    private readonly DataKeeper _dataKeeper;
    private readonly dynamic _schema;

    public DataExtractor(DataKeeper dataKeeper, dynamic schema)
    {
        _dataKeeper = dataKeeper;
        _schema = schema;
    }

    // Обрабатывает список HTML-документов
    public void Process(List<string> htmlContents)
    {
        foreach (var htmlContent in htmlContents)
        {
            var extractedData = ExtractData(htmlContent);
            if (extractedData != null)
            {
                // Отправляем данные на сохранение в DataKeeper
                _dataKeeper.AddData(extractedData);
            }
        }
    }

    // Извлекает данные из HTML-документа
    private Dictionary<string, string> ExtractData(string htmlContent)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlContent);

        var result = new Dictionary<string, string>();
        foreach (var field in _schema.documentSchema)
        {
            string xpath = field.cssSelector.ToString();
            var node = htmlDoc.DocumentNode.SelectSingleNode(xpath);
            if (node != null)
            {
                result[field.name.ToString()] = node.InnerText.Trim();
            }
            else
            {
                Console.WriteLine($"Узел не найден: {xpath}");
            }
        }

        return result;
    }

    // Преобразует CSS-селектор в XPath
    private string ConvertCssSelectorToXPath(string cssSelector)
    {
        var parts = cssSelector.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var xpath = new System.Text.StringBuilder();

        foreach (var part in parts)
        {
            if (part.Contains(">"))
            {
                // Обрабатываем прямые потомки
                var childParts = part.Split('>');
                foreach (var childPart in childParts)
                {
                    xpath.Append("/");
                    xpath.Append(ConvertCssPartToXPath(childPart.Trim()));
                }
            }
            else
            {
                xpath.Append("//");
                xpath.Append(ConvertCssPartToXPath(part.Trim()));
            }
        }

        return xpath.ToString();
    }

    // Преобразует часть CSS-селектора в XPath
    private string ConvertCssPartToXPath(string cssPart)
    {
        if (cssPart.Contains(":nth-child("))
        {
            // Обрабатываем :nth-child
            var element = cssPart.Split(':')[0];
            var index = cssPart.Split('(', ')')[1];
            return $"{element}[{index}]";
        }
        else if (cssPart.StartsWith("."))
        {
            // Обрабатываем классы
            var className = cssPart.Substring(1);
            return $"*[contains(@class, '{className}')]";
        }
        else if (cssPart.StartsWith("#"))
        {
            // Обрабатываем ID
            var id = cssPart.Substring(1);
            return $"*[@id='{id}']";
        }
        else
        {
            // Простой элемент
            return cssPart;
        }
    }
}