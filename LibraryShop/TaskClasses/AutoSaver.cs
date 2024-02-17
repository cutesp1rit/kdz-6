using LibraryShop.MechanicalClasses;

namespace LibraryShop;

public class AutoSaver
{
    private DateTime oldData;
    private List<Product> _products;
    
    public void WhenItChanged(object sender, DataEventArgs args)
    {
        if (oldData == new DateTime()) // если старое значение еще не задано
        {
            oldData = args.DataNewChanged;
            return;
        }

        TimeSpan diff = args.DataNewChanged - oldData;
        if ((args.DataNewChanged - oldData).TotalSeconds <= 15)
        {
            WorkJson.JsonSerialization(_products);
        }
    }
}