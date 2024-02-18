using System.Text;
using System.Text.Json.Serialization;
namespace LibraryShop;

// Product Additional Information
[Serializable]
public class ProductAddInf
{
    public event EventHandler<ProductEventArgs> SpecificationsPriceChanged;
    private string _specName;
    private double _specPrice;
    private bool _isCustom;
    public event EventHandler<DataEventArgs> Update;
    
    [JsonPropertyName("specName")]
    public string SpecName
    {
        get => _specName;
        set
        {
            _specName = value ?? throw new ArgumentNullException(nameof(value),
                "Ошибка инициализации \"specName\"");
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }
    }

    [JsonPropertyName("specPrice")]
    public double SpecPrice
    {
        get => _specPrice;
        set
        {
            _specPrice = value;
            OnSpecPriceChanged(this, new ProductEventArgs(_specPrice));    
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }
    }
    
    [JsonPropertyName("isCustom")]
    public bool IsCustom
    {
        get => _isCustom;
        set
        {
            _isCustom = value;
            OnSomethingChanged(this, new DataEventArgs(DateTime.Now));
        }
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
    
    protected virtual void OnSpecPriceChanged(object sender, ProductEventArgs args)
        => SpecificationsPriceChanged?.Invoke(sender, args);
    
    public ProductAddInf(string specName, double specPrice, bool isCustom)
    {
        _specName = specName ?? throw new ArgumentNullException(nameof(specName),
            "Ошибка инициализации \"specName\"");
        _specPrice = specPrice;
        _isCustom = isCustom;
    }

    public ProductAddInf()
    {
        _specName = "";
        _specPrice = 0;
        _isCustom = false;
    }

    public override string ToString()
    {
        return $"specName: {SpecName}, specPrice: {SpecPrice}, isCustom: {IsCustom}";
    }
}