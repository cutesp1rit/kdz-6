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
        
        if (!File.Exists(path)) throw new FormatException("Ошибка! Файла по этому пути не существует.");
        string JsonFileInString = File.ReadAllText(path);
        List<Product> products = JsonSerializer.Deserialize<List<Product>>(JsonFileInString);
        if (products == null || products.Count == 0)
        {
            throw new FormatException("Ошибка! Файл пуст!");
        }
        
        AutoSaver tracking = new AutoSaver(products);
        
        // теперь нужно подписать методы на события, чтобы при изменении цены спецификации
        // срабатывало событие.. и события-изменение тоже
        foreach (Product product in products)
        {
            foreach (var specProduct in product.Specifications)
            {
                specProduct.SpecificationsPriceChanged += product.ThisProductSpecificationsPriceChanged;
                specProduct.Update += specProduct.SomethingChanged;
                specProduct.Update += tracking.WhenItChanged;
            }
            product.Update += product.SomethingChanged;
            product.Update += tracking.WhenItChanged;
        }
        return products;
    }

    public static void JsonSerialization(List<Product> products, string path)
    {
        string jsonString = JsonSerializer.Serialize<List<Product>>(products);
        
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine(jsonString);
        }
        
        Console.WriteLine("Данные были успешно записаны!");
        Console.WriteLine("Нажмите любую кнопку, чтобы продлжить..");
        Console.ReadLine();
    }
}