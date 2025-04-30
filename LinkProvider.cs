using System.Collections.Generic;

/// <summary>
/// Provides access to filtered links from the LinkKeeper for processing.
/// </summary>
public class LinkProvider
{
    private readonly LinkKeeper _linkKeeper;

    /// <summary>
    /// Initializes a new instance of the LinkProvider class.
    /// </summary>
    /// <param name="linkKeeper">Component responsible for managing and filtering links</param>
    public LinkProvider(LinkKeeper linkKeeper)
    {
        _linkKeeper = linkKeeper;
    }

    /// <summary>
    /// Retrieves all filtered links that are ready for processing.
    /// </summary>
    /// <returns>List of filtered URLs</returns>
    public List<string> GetLinks()
    {
        return _linkKeeper.GetFilteredLinks();
    }
}