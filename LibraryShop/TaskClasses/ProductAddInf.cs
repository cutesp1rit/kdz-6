using System.Text;
using System.Text.Json.Serialization;
namespace LibraryShop;

// Product Additional Information
[Serializable]
public class ProductAddInf
{
    public event EventHandler<ProductEventArgs> SpecificationsPriceChanged;
    private string specName;
    private double specPrice;
    private bool isCustom;
    public event EventHandler<DataEventArgs> Update;
    
    [JsonPropertyName("specName")]
    public string SpecName
    {
        get => specName;
        set
        {
            specName = value;
            Update?.Invoke(this, new DataEventArgs(DateTime.Now));
        }
    }

    [JsonPropertyName("specPrice")]
    public double SpecPrice
    {
        get => specPrice;
        set
        {
            specPrice = value;
            SpecificationsPriceChanged?.Invoke(this, new ProductEventArgs(specPrice));    
            Update?.Invoke(this, new DataEventArgs(DateTime.Now));
        }
    }
    
    [JsonPropertyName("isCustom")]
    public bool IsCustom
    {
        get => isCustom;
        set
        {
            isCustom = value;
            Update?.Invoke(this, new DataEventArgs(DateTime.Now));
        }
    }
    
    public virtual void OnSomethingChanged(object sender, DataEventArgs args)
    {
        Console.WriteLine("Вы изменили часть объекта!");
        Console.WriteLine("Теперь он выглядит так:");
        Console.WriteLine(this);
        Console.WriteLine("Нажмите любую кнопку, чтобы продолжить: ");
        Console.ReadLine();
    }
    
    public ProductAddInf(string specName, double specPrice, bool isCustom)
    {
        this.specName = specName;
        this.specPrice = specPrice;
        this.isCustom = isCustom;
    }

    public ProductAddInf()
    {
        specName = "";
        specPrice = 0;
        isCustom = false;
    }

    public override string ToString()
    {
        return $"specName: {SpecName}, specPrice: {SpecPrice}, isCustom: {IsCustom}";
    }
}