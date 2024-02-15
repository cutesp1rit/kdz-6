using System.Text.Json;

namespace LibraryShop.MechanicalClasses;

public static class WorkJson
{
    public static List<Product> JsonDeserialization(string path)
    {
        // Альтернативное решение через контракты данных
        /* List<Product> products;
        DataContractJsonSerializer deser = new
            DataContractJsonSerializer(typeof(List<Product>));
        
        if (!File.Exists(path)) throw new FormatException("Ошибка! Такого файла не существует.");

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            products = (List<Product>) deser.ReadObject(fs);
        }

        if (products == null || products.Count == 0)
        {
            throw new FormatException("Ошибка! Файл пуст!");
        }
        return products; */ 
        
        if (!File.Exists(path)) throw new FormatException("Ошибка! Такого файла не существует.");
        string JsonFileInString = File.ReadAllText(path);
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(JsonFileInString);
        if (products == null || products.Count == 0)
        {
            throw new FormatException("Ошибка! Файл пуст!");
        }
        return products;
    }

    public static void JsonSerialization(string path)
    {
        
    }
}