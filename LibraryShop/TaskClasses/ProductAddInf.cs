using System.Text;
using System.Text.Json.Serialization;
namespace LibraryShop;

// Product Additional Information
[Serializable]
public class ProductAddInf
{
    private string specName;
    private double specPrice;
    private bool isCustom;
    
    [JsonPropertyName("specName")]
    public string SpecName
    {
        get => specName;
        set => specName = value;
    }

    [JsonPropertyName("specPrice")]
    public double SpecPrice
    {
        get => specPrice;
        set => specPrice = value;
    }

    [JsonPropertyName("isCustom")]
    public bool IsCustom
    {
        get => isCustom;
        set => isCustom = value;
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