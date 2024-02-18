using System.Text;
using System.Text.Json.Serialization;
namespace LibraryShop;

[Serializable]
public class Product
{
    private string _widgetId;
    private string _name;
    private int _quantity;
    private double _price;
    private bool _isAvailable; 
    private string _manufactureDate;
    private ProductAddInf[] _specifications;
    public event EventHandler<DataEventArgs> Update;

    [JsonPropertyName("widgetId")]
    public string WidgetId 
    {
        get => _widgetId;
        init => _widgetId = value ?? throw new ArgumentNullException(nameof(value),
            "Ошибка инициализации \"widgetId\"");
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get => _name;
        set 
        {
            _name = value ?? throw new ArgumentNullException(nameof(value),
                "Ошибка инициализации \"name\"");;
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }
    }

    [JsonPropertyName("quantity")]
    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value; 
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }
    }

    [JsonPropertyName("price")]
    public double Price
    {
        get => _price;
        init => _price = value;
    }
    
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable
    {
        get => _isAvailable;
        set 
        {
            _isAvailable = value;
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }    
    }

    [JsonPropertyName("manufactureDate")]
    public string ManufactureDate
    {
        get => _manufactureDate;
        set
        {
            _manufactureDate = value ?? throw new ArgumentNullException(nameof(value),
                "Ошибка инициализации \"manufactureDate\"");;
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }
    }
    
    [JsonPropertyName("specifications")]
    public ProductAddInf[] Specifications
    {
        get => _specifications;
        init => _specifications = value ?? throw new ArgumentNullException(nameof(value),
            "Ошибка инициализации \"specifications\"");
    }
    public virtual void ThisProductSpecificationsPriceChanged(object sender, ProductEventArgs args)
    {
        double newPrice = 0;
        foreach (ProductAddInf addInf in _specifications)
        {
            newPrice += addInf.SpecPrice;
        }
        _price = newPrice;
        Console.WriteLine("Вы изменили цену одной из спецификаций, поэтому общая цена тоже изменилась.");
        Console.WriteLine($"Новая цена: {Price}. Новая цена спецификации: {args.NewPrice}");
        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить..");
        Console.ReadLine();
    }

    public void SomethingChanged(object sender, DataEventArgs args)
    {
        Console.WriteLine("Вы изменили часть объекта!");
        Console.WriteLine("Теперь он выглядит так:");
        Console.WriteLine(this);
        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить: ");
        Console.ReadLine();
    }
    
    protected virtual void OnSomethingChanged(object sender, DataEventArgs args)
        => Update?.Invoke(sender, args);
    
    public Product(string widgetId, string name, int quantity, double price,
        bool isAvailable, string manufactureDate, ProductAddInf[] specifications)
    {
        _widgetId = widgetId ?? throw new ArgumentNullException(nameof(widgetId),
            "Ошибка инициализации \"widgetId\"");
        _name = name ?? throw new ArgumentNullException(nameof(name),
            "Ошибка инициализации \"name\"");
        _quantity = quantity;
        _price = price;
        _isAvailable = isAvailable;
        _manufactureDate = manufactureDate ?? throw new ArgumentNullException(nameof(manufactureDate),
            "Ошибка инициализации \"manufactureDate\"");
        _specifications = specifications ?? throw new ArgumentNullException(nameof(specifications),
            "Ошибка инициализации \"specifications\"");
    } 
    
    public Product()
    {
        _widgetId = "";
        _name = "";
        _quantity = 0;
        _price = 0;
        _isAvailable = false;
        _manufactureDate = "";
        _specifications = new ProductAddInf[0];
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