using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Filters URLs based on allowed domains to ensure only relevant links are processed.
/// </summary>
public class LinkFilter
{
    private readonly List<string> _allowedDomains;

    /// <summary>
    /// Initializes a new instance of the LinkFilter class.
    /// </summary>
    /// <param name="allowedDomains">List of domain names that are allowed to be processed</param>
    public LinkFilter(List<string> allowedDomains)
    {
        _allowedDomains = allowedDomains;
    }

    /// <summary>
    /// Filters a list of URLs, keeping only those that match the allowed domains.
    /// </summary>
    /// <param name="links">List of URLs to filter</param>
    /// <returns>Filtered list containing only URLs from allowed domains</returns>
    public List<string> Filter(List<string> links)
    {
        return links.Where(link => _allowedDomains.Any(domain => link.Contains(domain))).ToList();
    }
}