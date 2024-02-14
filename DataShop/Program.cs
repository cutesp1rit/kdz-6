using LibraryShop;
using LibraryShop.MechanicalClasses;
using LibraryShop.StyleConsole;

internal class Program
{
    public static void Main(string[] args)
    {
        List<Product> products = WorkJson.JsonDeserialization(".\\3V.json");
        Console.WriteLine(products[0].WidgetId);
    }
}