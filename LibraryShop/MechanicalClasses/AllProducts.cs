namespace LibraryShop.MechanicalClasses;

// Класс для реализации методов сортировки и прочих методов по заданию
public class AllProducts
{
    private List<Product> _products;

    public AllProducts(List<Product> products)
    {
        _products = products ?? throw new ArgumentNullException(nameof(products),
            "Ошибка инициализации \"products\"");
    }

    public AllProducts()
    {
        _products = new List<Product>();
    }

    public List<Product> Products
    {
        get => _products;
    }

    public void SortForString(string field)
    {
        for (int i = Products.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (String.Compare(Products[j].WhatIsFieldString(field), Products[j + 1].WhatIsFieldString(field),
                        StringComparison.Ordinal) > 0)
                {
                    (Products[j], Products[j + 1]) = (Products[j + 1], Products[j]);
                }
            }
        }
    }

    public void SortForQuantity()
    {
        for (int i = Products.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (Products[j].Quantity > Products[j + 1].Quantity)
                {
                    (Products[j], Products[j + 1]) = (Products[j + 1], Products[j]);
                }
            }
        }
    }
    
    public void SortForPrice()
    {
        for (int i = Products.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                if (Products[j].Price > Products[j + 1].Price)
                {
                    (Products[j], Products[j + 1]) = (Products[j + 1], Products[j]);
                }
            }
        }    
    }

    public void SortForIsAvailable()
    {
        // Сортировка происходит по принципу сначала все false, потом все true
        for (int i = Products.Count - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                // если первый элемент true, второй false -- swap
                if (Products[j].IsAvailable && !Products[j + 1].IsAvailable)
                {
                    (Products[j], Products[j + 1]) = (Products[j + 1], Products[j]);
                }
            }
        }
    }
}