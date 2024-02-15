namespace LibraryShop;
using System.Runtime.Serialization.Json; 
using System.Runtime.Serialization; 
// Product Additional Information
[DataContract]
public class ProductAddInf
{
    [DataMember]
    private string specName;
    [DataMember]
    private double specPrice;
    [DataMember]
    private bool isCustom;
    
    public string SpecName
    {
        get => specName;
        set => specName = value;
    }

    public double SpecPrice
    {
        get => specPrice;
        set => specPrice = value;
    }

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