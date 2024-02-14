namespace LibraryShop.MechanicalClasses;

// Класс для реализации методов сортировки и прочих методов по заданию
public class AllProducts
{
    private List<Product> _products;

    public AllProducts(List<Product> products)
    {
        _products = products;
    }

    public AllProducts()
    {
        _products = new List<Product>();
    }

    public List<Product> Products
    {
        get => _products;
        set => _products = value;
    }

    public void Sort()
    {
        
    }
}