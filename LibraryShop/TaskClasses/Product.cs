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
        set => name = value;
    }

    [JsonPropertyName("quantity")]
    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    [JsonPropertyName("price")]
    public double Price
    {
        get => price;
        set => price = value;
    }
    
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable
    {
        get => isAvailable;
        set => isAvailable = value;
    }

    [JsonPropertyName("manufactureDate")]
    public string ManufactureDate
    {
        get => manufactureDate;
        set => manufactureDate = value;
    }
    
    [JsonPropertyName("specifications")]
    public ProductAddInf[] Specifications
    {
        get => specifications;
        init => specifications = value;
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