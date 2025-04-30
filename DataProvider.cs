using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Responsible for fetching HTML content from URLs provided by LinkProvider.
/// </summary>
public class DataProvider
{
    private readonly LinkProvider _linkProvider;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the DataProvider class.
    /// </summary>
    /// <param name="linkProvider">Component that provides URLs for fetching</param>
    public DataProvider(LinkProvider linkProvider)
    {
        _linkProvider = linkProvider;
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Fetches HTML content from all available URLs.
    /// </summary>
    /// <returns>List of HTML content strings</returns>
    public async Task<List<string>> ProvideDataAsync()
    {
        var links = _linkProvider.GetLinks();
        var data = new List<string>();

        foreach (var link in links)
        {
            var content = await FetchDataAsync(link);
            if (content != null)
            {
                data.Add(content);
            }
        }

        return data;
    }

    /// <summary>
    /// Fetches HTML content from a specific URL.
    /// </summary>
    /// <param name="url">The URL to fetch content from</param>
    /// <returns>HTML content as string, or null if fetch fails</returns>
    private async Task<string> FetchDataAsync(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch
        {
            return null;
        }
    }
}