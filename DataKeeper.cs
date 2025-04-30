using System.Collections.Generic;

public class DataKeeper
{
    public List<Dictionary<string, string>> Data { get; private set; }

    public DataKeeper()
    {
        Data = new List<Dictionary<string, string>>();
    }

    // Добавляет данные в хранилище
    public void AddData(Dictionary<string, string> data)
    {
        Data.Add(data);
    }

    // Очищает хранилище (опционально)
    public void Clear()
    {
        Data.Clear();
    }
}