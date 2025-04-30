using System.Collections.Generic;

/// <summary>
/// Manages the collection of discovered links and applies filtering rules.
/// </summary>
public class LinkKeeper
{
    private readonly LinkFilter _linkFilter;
    private readonly List<string> _links;

    /// <summary>
    /// Initializes a new instance of the LinkKeeper class.
    /// </summary>
    /// <param name="linkFilter">Component responsible for filtering links based on domain rules</param>
    public LinkKeeper(LinkFilter linkFilter)
    {
        _linkFilter = linkFilter;
        _links = new List<string>();
    }

    /// <summary>
    /// Adds new links to the collection.
    /// </summary>
    /// <param name="links">List of URLs to add</param>
    public void AddLinks(List<string> links)
    {
        _links.AddRange(links);
    }

    /// <summary>
    /// Retrieves all links that pass the domain filtering rules.
    /// </summary>
    /// <returns>Filtered list of URLs</returns>
    public List<string> GetFilteredLinks()
    {
        return _linkFilter.Filter(_links);
    }
}