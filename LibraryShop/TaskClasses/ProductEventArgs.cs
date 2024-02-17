namespace LibraryShop;

public class ProductEventArgs : EventArgs
{
    public double NewPrice { get; init; }

    public ProductEventArgs(double price)
    {
        NewPrice = price;
    }

    public ProductEventArgs()
    {
        NewPrice = 0;
    }
}