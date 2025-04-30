using HtmlAgilityPack;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the extraction of structured data from HTML content based on a defined schema.
/// </summary>
public class DataExtractor
{
    private readonly DataKeeper _dataKeeper;
    private readonly dynamic _schema;

    /// <summary>
    /// Initializes a new instance of the DataExtractor class.
    /// </summary>
    /// <param name="dataKeeper">The component responsible for storing extracted data</param>
    /// <param name="schema">The schema defining what data to extract and where to find it</param>
    public DataExtractor(DataKeeper dataKeeper, dynamic schema)
    {
        _dataKeeper = dataKeeper;
        _schema = schema;
    }

    /// <summary>
    /// Processes a list of HTML documents and extracts data according to the schema.
    /// </summary>
    /// <param name="htmlContents">List of HTML documents to process</param>
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

    /// <summary>
    /// Extracts structured data from a single HTML document using the defined schema.
    /// </summary>
    /// <param name="htmlContent">The HTML content to process</param>
    /// <returns>Dictionary containing extracted data fields and their values</returns>
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
                Console.WriteLine($"Node not found: {xpath}");
            }
        }

        return result;
    }

    /// <summary>
    /// Converts a CSS selector to an XPath expression.
    /// </summary>
    /// <param name="cssSelector">The CSS selector to convert</param>
    /// <returns>Equivalent XPath expression</returns>
    private string ConvertCssSelectorToXPath(string cssSelector)
    {
        var parts = cssSelector.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var xpath = new System.Text.StringBuilder();

        foreach (var part in parts)
        {
            if (part.Contains(">"))
            {
                // Handle direct descendants
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

    /// <summary>
    /// Converts a part of a CSS selector to an XPath expression.
    /// Handles various CSS selector patterns including classes, IDs, and nth-child selectors.
    /// </summary>
    /// <param name="cssPart">Part of the CSS selector to convert</param>
    /// <returns>Equivalent XPath expression part</returns>
    private string ConvertCssPartToXPath(string cssPart)
    {
        if (cssPart.Contains(":nth-child("))
        {
            // Handle nth-child selectors
            var element = cssPart.Split(':')[0];
            var index = cssPart.Split('(', ')')[1];
            return $"{element}[{index}]";
        }
        else if (cssPart.StartsWith("."))
        {
            // Handle class selectors
            var className = cssPart.Substring(1);
            return $"*[contains(@class, '{className}')]";
        }
        else if (cssPart.StartsWith("#"))
        {
            // Handle ID selectors
            var id = cssPart.Substring(1);
            return $"*[@id='{id}']";
        }
        else
        {
            // Simple element selector
            return cssPart;
        }
    }
}