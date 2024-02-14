using System.Text.Json;
using System.Runtime.Serialization.Json; 
using System.Runtime.Serialization; 

namespace LibraryShop.MechanicalClasses;

public static class WorkJson
{
    public static List<Product> JsonDeserialization(string path)
    {
        // Десериализация
        List<Product> products;
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
        return products;
    }
}