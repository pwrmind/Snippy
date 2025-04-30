using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Main program class that orchestrates the web scraping and data extraction process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Initializes components and runs the data collection process.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Initialize core components
        var dataKeeper = new DataKeeper(); // Stores extracted data
        var linkFilter = new LinkFilter(new List<string> { "example.com" }); // Filters links by domain
        var linkKeeper = new LinkKeeper(linkFilter); // Manages discovered links
        var linkProvider = new LinkProvider(linkKeeper); // Provides links for processing
        var dataProvider = new DataProvider(linkProvider); // Fetches HTML content
        var linkExtractor = new LinkExtractor(linkKeeper); // Extracts new links from HTML

        // Define data extraction schema
        // This schema specifies what data to extract and where to find it
        var schema = new
        {
            schemaVersion = "V0",
            documentSchema = new[]
            {
                new { name = "title", cssSelector = "/html/body/div/h1" }, // Extracts page title
                new { name = "field", cssSelector = "/html/body/div/p" }, // Extracts paragraph content
            }
        };
        var dataExtractor = new DataExtractor(dataKeeper, schema); // Extracts data based on schema

        // Add initial URL to start the crawling process
        linkKeeper.AddLinks(new List<string> { "https://example.com" });

        // Main data collection loop
        // Processes a limited number of iterations for demonstration
        for (int i = 0; i < 3; i++)
        {
            // Fetch HTML content from URLs
            var data = await dataProvider.ProvideDataAsync();
            
            // Extract new links and structured data from HTML
            linkExtractor.Process(data);
            dataExtractor.Process(data);
        }

        // Display collected data
        Console.WriteLine("Сollected data:");
        foreach (var item in dataKeeper.Data)
        {
            foreach (var kvp in item)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}