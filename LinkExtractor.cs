using HtmlAgilityPack;
using System.Collections.Generic;

public class LinkExtractor
{
    private readonly LinkKeeper _linkKeeper;

    public LinkExtractor(LinkKeeper linkKeeper)
    {
        _linkKeeper = linkKeeper;
    }

    public void Process(List<string> htmlContents)
    {
        foreach (var htmlContent in htmlContents)
        {
            var links = ExtractLinks(htmlContent);
            _linkKeeper.AddLinks(links);
        }
    }

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