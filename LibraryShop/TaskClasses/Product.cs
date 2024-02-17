using System.Text;
using System.Text.Json.Serialization;
namespace LibraryShop;

[Serializable]
public class Product
{
    private string widgetId;
    private string name;
    private int quantity;
    private double price;
    private bool isAvailable; 
    private string manufactureDate;
    private ProductAddInf[] specifications;
    public event EventHandler<DataEventArgs> SomethingChanged;

    [JsonPropertyName("widgetId")]
    public string WidgetId 
    {
        get => widgetId;
        init => widgetId = value;
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get => name;
        set 
        {
            name = value;
            SomethingChanged?.Invoke(this, new DataEventArgs(DateTime.Now));
        }
    }

    [JsonPropertyName("quantity")]
    public int Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            SomethingChanged?.Invoke(this, new DataEventArgs(DateTime.Now));
        }
    }

    [JsonPropertyName("price")]
    public double Price
    {
        get => price;
        init => price = value;
    }
    
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable
    {
        get => isAvailable;
        set 
        { 
            isAvailable = value;
            SomethingChanged?.Invoke(this, new DataEventArgs(DateTime.Now));
        }    
    }

    [JsonPropertyName("manufactureDate")]
    public string ManufactureDate
    {
        get => manufactureDate;
        set
        {
            manufactureDate = value;
            SomethingChanged?.Invoke(this, new DataEventArgs(DateTime.Now));
        }
    }
    
    [JsonPropertyName("specifications")]
    public ProductAddInf[] Specifications
    {
        get => specifications;
        init => specifications = value;
    }
    public virtual void ThisProductSpecificationsPriceChanged(object sender, ProductEventArgs args)
    {
        double newPrice = 0;
        foreach (ProductAddInf addInf in specifications)
        {
            newPrice += addInf.SpecPrice;
        }
        price = newPrice;
        Console.WriteLine("Вы изменили цену одной из спецификаций, поэтому общая цена тоже изменилась.");
        Console.WriteLine($"Новая цена: {Price}. Новая цена спецификации: {args.NewPrice}");
        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить..");
        Console.ReadLine();
    }

    public virtual void OnSomethingChanged(object sender, DataEventArgs args)
    {
        Console.WriteLine("Вы изменили часть объекта!");
        Console.WriteLine("Теперь он выглядит так:");
        Console.WriteLine(this);
        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить: ");
        Console.ReadLine();
    }
    
    public Product(string widgetId, string name, int quantity, double price,
        bool isAvailable, string manufactureDate, ProductAddInf[] specifications)
    {
        this.widgetId = widgetId;
        this.name = name;
        this.quantity = quantity;
        this.price = price;
        this.isAvailable = isAvailable;
        this.manufactureDate = manufactureDate;
        this.specifications = specifications;
    } 
    
    public Product()
    {
        widgetId = "";
        name = "";
        quantity = 0;
        price = 0;
        isAvailable = false;
        manufactureDate = "";
        specifications = new ProductAddInf[0];
    }

    public string WhatIsFieldString(string field)
    {
        if (field == "widgetId")
        {
            return WidgetId;
        }
        
        if (field == "name")
        {
            return Name;
        }
        
        if (field == "manufactureDate")
        {
            return ManufactureDate;
        }

        return "";
    }

    public override string ToString()
    {
        return $"widgetId: {WidgetId}, name: {Name}, quantity: {Quantity}, price: {Price}, " +
               $"isAvailable: {IsAvailable}, ManufactureDate: {ManufactureDate}..";
    }

    public string ToJSON()
    {
        StringBuilder stringJson = new StringBuilder($"  {{\n    \"widgetId\": \"{WidgetId}\",\n" +
               $"    \"name\": \"{Name}\",\n    \"quantity\": {Quantity},\n    \"price\": {Price},\n" +
               $"    \"isAvailable\": {IsAvailable},\n    \"manufactureDate\": \"{ManufactureDate}\",\n" +
               $"    \"specifications\": [");

        for (int i = 0; i < Specifications.Length; i++)
        {
            stringJson.Append(
                $"\n      {{\n        \"specName\": \"{Specifications[i].SpecName}\",\n        \"specPrice\": " +
                $"{Specifications[i].SpecPrice},\n        \"isCustom\": {Specifications[i].IsCustom}\n      }}");
            if (i != Specifications.Length - 1)
            {
                stringJson.Append(",");
            }
        }

        stringJson.Append("\n    ]\n  }");

        return stringJson.ToString();
    }
}