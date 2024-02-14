namespace LibraryShop;
using System.Runtime.Serialization.Json; 
using System.Runtime.Serialization; 

[DataContract]
public class Product
{
    [DataMember]
    private string widgetId;
    [DataMember]
    private string name;
    [DataMember]
    private int quantity;
    [DataMember]
    private double price;
    [DataMember]
    private bool isAvailable; 
    [DataMember]
    private string manufactureDate;
    [DataMember]
    private ProductAddInf[] specifications;

    public string WidgetId => widgetId;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }

    public double Price
    {
        get => price;
        set => price = value;
    }

    public bool IsAvailable
    {
        get => isAvailable;
        set => isAvailable = value;
    }

    public string ManufactureDate
    {
        get => manufactureDate;
        set => manufactureDate = value;
    }
    
    public ProductAddInf[] Specifications => specifications;

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
    
    public string TOJSON()
    { 
        return "";
    }
}