using LibraryShop.MechanicalClasses;

namespace LibraryShop;

public class AutoSaver
{
    private DateTime oldData;
    private List<Product> _products;
    
    public AutoSaver(List<Product> products)
    {
        _products = products;
        oldData = new DateTime();
    }

    public AutoSaver()
    {
        _products = new List<Product>();
        oldData = new DateTime();
    }
    
    public void WhenItChanged(object sender, DataEventArgs args)
    {
        if (oldData == new DateTime()) // если старое значение еще не задано
        {
            oldData = args.DataNewChanged;
            return;
        }

        if ((args.DataNewChanged - oldData).TotalSeconds <= 15)
        {
            WorkJson.JsonSerialization(_products, ".\\Products_tmp.json");
        }
    }
}