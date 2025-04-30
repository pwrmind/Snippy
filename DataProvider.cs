using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class DataProvider
{
    private readonly LinkProvider _linkProvider;
    private readonly HttpClient _httpClient;

    public DataProvider(LinkProvider linkProvider)
    {
        _linkProvider = linkProvider;
        _httpClient = new HttpClient();
    }

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