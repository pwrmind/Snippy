using System.Collections.Generic;
using System.Linq;

public class LinkFilter
{
    private readonly List<string> _allowedDomains;

    public LinkFilter(List<string> allowedDomains)
    {
        _allowedDomains = allowedDomains;
    }

    public List<string> Filter(List<string> links)
    {
        return links.Where(link => _allowedDomains.Any(domain => link.Contains(domain))).ToList();
    }
}