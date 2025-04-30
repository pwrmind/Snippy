using System.Collections.Generic;

public class LinkKeeper
{
    private readonly LinkFilter _linkFilter;
    private readonly List<string> _links;

    public LinkKeeper(LinkFilter linkFilter)
    {
        _linkFilter = linkFilter;
        _links = new List<string>();
    }

    public void AddLinks(List<string> links)
    {
        _links.AddRange(links);
    }

    public List<string> GetFilteredLinks()
    {
        return _linkFilter.Filter(_links);
    }
}