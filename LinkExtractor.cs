using HtmlAgilityPack;
using System.Collections.Generic;

/// <summary>
/// Extracts links from HTML content and adds them to the LinkKeeper for further processing.
/// </summary>
public class LinkExtractor
{
    private readonly LinkKeeper _linkKeeper;

    /// <summary>
    /// Initializes a new instance of the LinkExtractor class.
    /// </summary>
    /// <param name="linkKeeper">Component responsible for storing and managing discovered links</param>
    public LinkExtractor(LinkKeeper linkKeeper)
    {
        _linkKeeper = linkKeeper;
    }

    /// <summary>
    /// Processes a list of HTML documents to extract links.
    /// </summary>
    /// <param name="htmlContents">List of HTML documents to process</param>
    public void Process(List<string> htmlContents)
    {
        foreach (var htmlContent in htmlContents)
        {
            var links = ExtractLinks(htmlContent);
            _linkKeeper.AddLinks(links);
        }
    }

    /// <summary>
    /// Extracts all href attributes from anchor tags in the HTML content.
    /// </summary>
    /// <param name="htmlContent">HTML content to process</param>
    /// <returns>List of extracted URLs</returns>
    private List<string> ExtractLinks(string htmlContent)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlContent);

        var links = new List<string>();
        foreach (var node in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
        {
            var href = node.GetAttributeValue("href", string.Empty);
            if (!string.IsNullOrEmpty(href))
            {
                links.Add(href);
            }
        }

        return links;
    }
}