using System.Collections.Generic;

/// <summary>
/// Stores and manages the extracted data from web pages.
/// </summary>
public class DataKeeper
{
    /// <summary>
    /// Gets the collection of extracted data items.
    /// Each item is a dictionary of field names and their corresponding values.
    /// </summary>
    public List<Dictionary<string, string>> Data { get; private set; }

    /// <summary>
    /// Initializes a new instance of the DataKeeper class.
    /// </summary>
    public DataKeeper()
    {
        Data = new List<Dictionary<string, string>>();
    }

    /// <summary>
    /// Adds a new data item to the storage.
    /// </summary>
    /// <param name="data">Dictionary containing field names and their values</param>
    public void AddData(Dictionary<string, string> data)
    {
        Data.Add(data);
    }

    /// <summary>
    /// Clears all stored data from the collection.
    /// </summary>
    public void Clear()
    {
        Data.Clear();
    }
}