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
        
        AutoSaver tracking = new AutoSaver();
        // теперь нужно подписать методы на события, чтобы при изменении цены спецификации срабатывало событие..
        foreach (Product product in products)
        {
            foreach (var specProduct in product.Specifications)
            {
                specProduct.SpecificationsPriceChanged += product.ThisProductSpecificationsPriceChanged;
                specProduct.SomethingChanged += specProduct.OnSomethingChanged;
                specProduct.SomethingChanged += tracking.WhenItChanged;
            }
            
            product.SomethingChanged += product.OnSomethingChanged;
            product.SomethingChanged += tracking.WhenItChanged;
        }
        return products;
    }

    public static void JsonSerialization(List<Product> products)
    {
        
    }
}