using System.Collections.Generic;

public class LinkProvider
{
    private readonly LinkKeeper _linkKeeper;

    public LinkProvider(LinkKeeper linkKeeper)
    {
        _linkKeeper = linkKeeper;
    }

    public List<string> GetLinks()
    {
        return _linkKeeper.GetFilteredLinks();
    }
}